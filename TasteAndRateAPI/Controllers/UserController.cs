﻿using TasteAndRateAPI.Models.DTOs;
using TasteAndRateAPI.Models.DTOs.UserDto;
using TasteAndRateAPI.Repository;
using TasteAndRateAPI.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace TasteAndRateAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        protected ResponseApi _reponseApi;
        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _reponseApi = new ResponseApi();
            _mapper = mapper;
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult GetUsers()
        {
            var userList = _userRepository.GetUsers();
            var userListDto = new List<UserDto>();

            foreach (var user in userList)
            {
                userListDto.Add(_mapper.Map<UserDto>(user));
            }

            return Ok(userListDto);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{userId:int}", Name = "GetUser")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUser(string userId)
        {
            var user = _userRepository.GetUser(userId);
            if (user == null) { return NotFound(); }

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register(UserRegistrationDto userRegistrationDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { error = "Incorrect Input", message = ModelState });
            }

            if (!_userRepository.IsUniqueUser(userRegistrationDto.Email))
            {
                _reponseApi.StatusCode = HttpStatusCode.BadRequest;
                _reponseApi.IsSuccess = false;
                _reponseApi.ErrorMessages.Add("Email already exists");
                return BadRequest();
            }

            var newUser = await _userRepository.Register(userRegistrationDto);
            if (newUser == null)
            {
                _reponseApi.StatusCode = HttpStatusCode.BadRequest;
                _reponseApi.IsSuccess = false;
                _reponseApi.ErrorMessages.Add("Error registering the user");
                return BadRequest();
            }

            _reponseApi.StatusCode = HttpStatusCode.OK;
            _reponseApi.IsSuccess = true;
            return Ok(_reponseApi);
        }


        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var responseLogin = await _userRepository.Login(userLoginDto);

            if (responseLogin.User == null || string.IsNullOrEmpty(responseLogin.Token))
            {
                _reponseApi.StatusCode = HttpStatusCode.BadRequest;
                _reponseApi.IsSuccess = false;
                _reponseApi.ErrorMessages.Add("Incorrect user and password");
                return BadRequest(_reponseApi);
            }

            _reponseApi.StatusCode = HttpStatusCode.OK;
            _reponseApi.IsSuccess = true;
            _reponseApi.Result = responseLogin;
            return Ok(_reponseApi);
        }
        
        [HttpGet("validar")]
        [Authorize] // Requiere un token válido
        public IActionResult ValidarToken()
        {
            var identity = HttpContext.User.Identity;

            if (identity != null && identity.IsAuthenticated)
            {
                _reponseApi.StatusCode = HttpStatusCode.OK;
                _reponseApi.IsSuccess = true;
                //_reponseApi.Result = responseLogin;
                return Ok(_reponseApi);
            }

            return Unauthorized(new { mensaje = "Token inválido o expirado" });
        }
    }
}
