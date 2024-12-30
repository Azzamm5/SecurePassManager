using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiProject.DTOs;
using FluentValidation;

namespace apiProject.Validators
{
    public class AccountUpdateDtoValidator : AbstractValidator<AccountUpdateDto>
    {
        public AccountUpdateDtoValidator()
        {
            RuleFor(x => x.Service)
                .NotEmpty().WithMessage("Le nom du compte est requis.")
                .MaximumLength(50).WithMessage("Le nom du compte ne doit pas dépasser 50 caractères.");

            RuleFor(x => x.Username)
                    .NotEmpty().WithMessage("L'email est requis.")
                    .EmailAddress().WithMessage("L'email doit être valide.");

            RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Le mot de passe est requis.")
            .MinimumLength(8).WithMessage("Le mot de passe doit contenir au moins 8 caractères.")
            .MaximumLength(16).WithMessage("Le mot de passe doit contenir 16 caractères au max .")
            .Matches("[A-Z]").WithMessage("Le mot de passe doit contenir au moins une majuscule.")
            .Matches("[0-9]").WithMessage("Le mot de passe doit contenir au moins un chiffre.");
        }
        
    }
}