﻿namespace AUTH_SERVICE.Models.DTOS
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = string.Empty;

        public UserDto User { get; set; } = default!;
    }
}
