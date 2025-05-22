using APBD_tutorial11.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_tutorial11.Data;

public class DatabaseContext : DbContext
{
    
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    
    protected DatabaseContext() { }
    
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>().HasData(new List<Doctor>
        {
            new Doctor() { IdDoctor = 1, FirstName = "Jane", LastName = "Doe", Email = "jane.doe@gmail.com" },
            new Doctor() { IdDoctor = 2, FirstName = "Bob", LastName = "Smith", Email = "bob.smith@gmail.com" },
            new Doctor()
                { IdDoctor = 3, FirstName = "Steven", LastName = "Blackwood", Email = "steven.blackwood@gmail.com" }
        });

        modelBuilder.Entity<Medicament>().HasData(new List<Medicament>
        {
            new Medicament { IdMedicament = 1, Name = "Aspirin", Description = "Pain reliever", Type = "Tablet" },
            new Medicament { IdMedicament = 2, Name = "Amoxicillin", Description = "Antibiotic", Type = "Capsule" },
            new Medicament
                { IdMedicament = 3, Name = "Insulin", Description = "Diabetes treatment", Type = "Injection" }
        });

        modelBuilder.Entity<Patient>().HasData(new List<Patient>
        {
            new Patient
            {
                IdPatient = 1, FirstName = "John", LastName = "Smith", Birthdate = new DateTime(1985, 5, 12)
            },
            new Patient { IdPatient = 2, FirstName = "Anna", LastName = "Grey", Birthdate = new DateTime(1990, 8, 23) },
            new Patient { IdPatient = 3, FirstName = "Mark", LastName = "Brown", Birthdate = new DateTime(1978, 3, 5) }

        });

        modelBuilder.Entity<Prescription>().HasData(new List<Prescription>
        {
            new Prescription
            {
                IdPrescription = 1, Date = new DateTime(2025, 5, 1), DueDate = new DateTime(2025, 5, 10), IdPatient = 1,
                IdDoctor = 1
            },
            new Prescription
            {
                IdPrescription = 2, Date = new DateTime(2025, 5, 3), DueDate = new DateTime(2025, 5, 15), IdPatient = 2,
                IdDoctor = 2
            },
            new Prescription
            {
                IdPrescription = 3, Date = new DateTime(2025, 5, 5), DueDate = new DateTime(2025, 5, 20), IdPatient = 3,
                IdDoctor = 1
            },
            new Prescription
            {
                IdPrescription = 4, Date = new DateTime(2025, 5, 2), DueDate = new DateTime(2025, 5, 8), IdPatient = 1,
                IdDoctor = 3
            },
            new Prescription
            {
                IdPrescription = 5, Date = new DateTime(2025, 5, 4), DueDate = new DateTime(2025, 5, 25), IdPatient = 1,
                IdDoctor = 2
            }
        });

        modelBuilder.Entity<PrescriptionMedicament>().HasData(new List<PrescriptionMedicament>
        {
            new PrescriptionMedicament { IdMedicament = 1, IdPrescription = 1, Dose = 1, Details = "Take after meal" },
            new PrescriptionMedicament { IdMedicament = 2, IdPrescription = 2, Dose = 2, Details = "Twice daily" },
            new PrescriptionMedicament { IdMedicament = 3, IdPrescription = 3, Dose = 1, Details = "Before sleep" },
            new PrescriptionMedicament { IdMedicament = 1, IdPrescription = 4, Dose = 3, Details = "Morning only" },
            new PrescriptionMedicament { IdMedicament = 2, IdPrescription = 5, Dose = 1, Details = "Evening only" },
            new PrescriptionMedicament { IdMedicament = 3, IdPrescription = 5, Dose = 2, Details = "Also during lunch" }
        });

    }
    
    
}