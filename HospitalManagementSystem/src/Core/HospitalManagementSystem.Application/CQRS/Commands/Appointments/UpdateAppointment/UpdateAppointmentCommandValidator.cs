namespace HospitalManagementSystem.Application.CQRS.Commands.Appointments.UpdateAppointment;

public class UpdateAppointmentCommandValidator : AbstractValidator<UpdateAppointmentCommandRequest>
{
    public UpdateAppointmentCommandValidator()
    {
        RuleFor(x => x.DoctorId).NotEmpty().WithMessage("DoctorId is required.");
        RuleFor(x => x.StartTime).Must(BeWithinOperatingHours).WithMessage("Appointment start time must be within operating hours (08:00 - 20:00).");
        RuleFor(x => x.EndTime).Must(BeWithinOperatingHours).WithMessage("Appointment end time must be within operating hours (08:00 - 20:00).");
        RuleFor(x => x).Must(HaveValidDuration).WithMessage("Appointment duration must be between 10-30 minutes!");
        RuleFor(a => a.Remarks)
            .MaximumLength(500).WithMessage("Maximum length is 500 characters for your remarks");
    }
    private bool BeWithinOperatingHours(DateTime dateTime)
    {
        var startOfDay = new TimeSpan(8, 0, 0); // 08:00
        var endOfDay = new TimeSpan(20, 0, 0); // 20:00

        var timeOfDay = dateTime.TimeOfDay;
        return timeOfDay >= startOfDay && timeOfDay <= endOfDay;
    }

    private bool HaveValidDuration(UpdateAppointmentCommandRequest request)
    {
        return request.EndTime > request.StartTime &&
            (request.EndTime - request.StartTime).TotalMinutes >= 10 &&
            (request.EndTime - request.StartTime).TotalMinutes <= 30;
    }
}
