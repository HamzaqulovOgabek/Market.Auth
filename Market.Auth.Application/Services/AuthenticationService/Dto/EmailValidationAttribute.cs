﻿using System.ComponentModel.DataAnnotations;

namespace Market.Auth.Application.Services.AuthenticationService;

public class EmailValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var dto = validationContext.ObjectInstance as UserRegistrationDto;


        if (dto == null || 
            string.IsNullOrWhiteSpace(dto.Email) || 
            !dto.IsValidEmail(dto.Email)
        )
        {
            return new ValidationResult("Invalid Email");
        }

        return base.IsValid(value, validationContext);
    }
}
