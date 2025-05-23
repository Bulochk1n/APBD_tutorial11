using APBD_tutorial11.DTOs;
using APBD_tutorial11.Models;
using APBD_tutorial11.Services;
using Microsoft.EntityFrameworkCore;

namespace AppUnitTests;

public class DbServiceTests
{
    [Fact]
    public async Task IssueNewPrescription_ShouldThrowException_WhenDueDateBeforeDate()
    {
        
        var context = FakeDatabaseContext.GetFakeContext("Test1");
        var service = new DbService(context);

        var prescriptionDto = new PrescriptionDto
        {
            Date = DateTime.Today,
            DueDate = DateTime.Today.AddDays(-1), // неверная дата
            Medicaments = new List<MedicamentDto>(),
            Patient = new PatientDto { IdPatient = 1 },
            Doctor = new DoctorDto { IdDoctor = 1 }
        };

        
        await Assert.ThrowsAsync<Exception>(() => service.IssueNewPrescription(prescriptionDto));
    }

    [Fact]
    public async Task IssueNewPrescription_ShouldThrowException_WhenNumberOfMedicationsMoreThanTen()
    {
        var context = FakeDatabaseContext.GetFakeContext("Test2");
        var service = new DbService(context);
        
        var prescriptionDto = new PrescriptionDto
        {
            Date = DateTime.Today,
            DueDate = DateTime.Today.AddDays(-1), // неверная дата
            Medicaments = new List<MedicamentDto>()
            {
                new MedicamentDto { IdMedicament = 1 },
                new MedicamentDto { IdMedicament = 2 },
                new MedicamentDto { IdMedicament = 3 },
                new MedicamentDto { IdMedicament = 4 },
                new MedicamentDto { IdMedicament = 5 },
                new MedicamentDto { IdMedicament = 6 },
                new MedicamentDto { IdMedicament = 7 },
                new MedicamentDto { IdMedicament = 8 },
                new MedicamentDto { IdMedicament = 9 },
                new MedicamentDto { IdMedicament = 10 },
                new MedicamentDto { IdMedicament = 11 }
            },
            Patient = new PatientDto { IdPatient = 1 },
            Doctor = new DoctorDto { IdDoctor = 1 }
        };
        
        await Assert.ThrowsAsync<Exception>(() => service.IssueNewPrescription(prescriptionDto));
        
    }

    [Fact]
    public async Task IssueNewPrescription_ShouldAddNewPatient_WhenPatientDoesNotExist()
    {
        var context = FakeDatabaseContext.GetFakeContext("Test3");
        var service = new DbService(context);

        var prescriptionDto = new PrescriptionDto
        {
            Patient = new PatientDto
                { FirstName = "Gregory", LastName = "Kurg", Birthdate = new DateTime(1985, 5, 12) },
            Medicaments = new List<MedicamentDto>()
            {
                new MedicamentDto { IdMedicament = 1, Description = "AAA", Dose = 2},
            },
            Date = new DateTime(2025, 5, 5),
            Doctor = new DoctorDto { IdDoctor = 1 },
            DueDate = DateTime.Today.AddDays(-1),
            
        };

        context.Medicaments.Add(new Medicament()
        {
            IdMedicament = 5,
            Name = "Ibuprofen",
            Description = "AAA",
            Type = "Tablet"
        });
        
        await context.SaveChangesAsync();
        
        await service.IssueNewPrescription(prescriptionDto);
        
        var patient = await context.Patients.FirstOrDefaultAsync(p => p.FirstName == "Gregory");
        Assert.NotNull(patient);
    }

    [Fact]
    public async Task IssueNewPrescription_ShouldReturnError_WhenMedicamentDoesNotExist()
    {
        var context = FakeDatabaseContext.GetFakeContext("Test4");
        var service = new DbService(context);
        
        var prescriptionDto = new PrescriptionDto
        {
            Patient = new PatientDto
                { FirstName = "Gregory", LastName = "Kurg", Birthdate = new DateTime(1985, 5, 12) },
            Medicaments = new List<MedicamentDto>()
            {
                new MedicamentDto { IdMedicament = 99999, Description = "AAA", Dose = 2},
            },
            Date = new DateTime(2025, 5, 5),
            Doctor = new DoctorDto { IdDoctor = 1 },
            DueDate = DateTime.Today.AddDays(-1),
            
        };

        
        await Assert.ThrowsAsync<Exception>(() => service.IssueNewPrescription(prescriptionDto));
        


    }
    
    
}