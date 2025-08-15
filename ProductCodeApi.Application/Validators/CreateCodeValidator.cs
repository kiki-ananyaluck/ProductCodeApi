using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using System.Text.RegularExpressions;

namespace ProductCodeApi.Application.Validators;

public class CreateCodeValidator : AbstractValidator<CreateCodeRequest>
{
    private static readonly Regex NoDash = new("^[A-Z0-9]{30}$");
    private static readonly Regex WithDash = new("^[A-Z0-9]{5}(-[A-Z0-9]{5}){5}$");

    public CreateCodeValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .Must(c => NoDash.IsMatch(c.ToUpper()) || WithDash.IsMatch(c.ToUpper()))
            .WithMessage("รูปแบบรหัสไม่ถูกต้อง");
    }

}