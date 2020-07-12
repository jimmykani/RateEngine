using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using RateEngine.Core.Validator;

namespace RateEngine.Application.Features.Rate.Get
{
    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(m => m.EntryTime).NotEmpty().NotNull();
            RuleFor(m => m.ExitTime).NotEmpty().NotNull();
            RuleFor(m => DateValidator.BeAValidDate(m.EntryTime));
            RuleFor(m => DateValidator.BeAValidDates(m.EntryTime, m.ExitTime));
        }
    }
}
