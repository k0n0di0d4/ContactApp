﻿namespace ContactApp.Models
{
    public class UserRequest
    {
        public required string Email { get; set; } = string.Empty;
        public required string Password { get; set; } = string.Empty;
    }
}
