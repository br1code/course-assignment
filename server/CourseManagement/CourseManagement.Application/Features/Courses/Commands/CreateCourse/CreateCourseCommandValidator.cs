﻿using FluentValidation;
using CourseManagement.Application.Interfaces;

namespace CourseManagement.Application.Features.Courses.Commands.CreateCourse;

public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public const string DUPLICATED_COURSE_ERROR_MESSAGE = "A course with the same subject and course number already exists.";

    private readonly ICourseRepository _repository;

    public CreateCourseCommandValidator(ICourseRepository repository)
    {
        _repository = repository;

        RuleFor(x => x.Subject)
            .NotEmpty()
            .WithMessage("Subject is required.");

        RuleFor(x => x.CourseNumber)
            .NotEmpty()
            .WithMessage("CourseNumber is required.")
            .Length(3)
            .WithMessage("CourseNumber must be exactly 3 characters.")
            .Matches(@"^\d{3}$")
            .WithMessage("CourseNumber must be a three-digit, zero-padded integer like '033'.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required.");

        RuleFor(x => x)
            .MustAsync(BeUniqueCourseAsync)
            .WithMessage(DUPLICATED_COURSE_ERROR_MESSAGE);
    }

    private async Task<bool> BeUniqueCourseAsync(CreateCourseCommand command, CancellationToken cancellationToken)
    {
        return !await _repository.CourseExistsAsync(command.Subject, command.CourseNumber, cancellationToken);
    }
}
