using APBD_tutorial11.Data;
using APBD_tutorial11.DTOs;
using APBD_tutorial11.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace APBD_tutorial11.Services;

public class DbService : IDbService
{
    
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext context)
    {
        _context = context;
    }


    public async Task IssueNewPrescription(PrescriptionDto prescriptionDto)
    {

        if (prescriptionDto.DueDate < prescriptionDto.Date)
        {
            throw new Exception("Due date cannot be earlier than date");
        }

        if (prescriptionDto.Medicaments.Count > 10)
        {
            throw new Exception("Maximum number of medicaments is 10");
        }
        
        foreach (var medicament in prescriptionDto.Medicaments)
        {
            var existsMedicament = await _context.Medicaments.AnyAsync(m => m.IdMedicament == medicament.IdMedicament);
            if (!existsMedicament)
            {
                throw new Exception($"Medicament with id {medicament.IdMedicament} not found");
            }
        }
        
        var existsPatient = await _context.Patients.AnyAsync(p => p.IdPatient == prescriptionDto.Patient.IdPatient);
        if (!existsPatient)
        {
            await _context.Patients.AddAsync(new Patient() { IdPatient = prescriptionDto.Patient.IdPatient,
                                                        FirstName = prescriptionDto.Patient.FirstName,
                                                        LastName = prescriptionDto.Patient.LastName,
                                                        Birthdate = prescriptionDto.Patient.Birthdate});
            
        }
        await _context.SaveChangesAsync();



        var prescription = new Prescription()
        {
            Date = prescriptionDto.Date,
            DueDate = prescriptionDto.DueDate,
            IdPatient = prescriptionDto.Patient.IdPatient,
            IdDoctor = prescriptionDto.Doctor.IdDoctor
        };
        
        await _context.Prescriptions.AddAsync(prescription);
        await _context.SaveChangesAsync();
        foreach (var medicament in prescriptionDto.Medicaments)
        {
            await _context.PrescriptionMedicaments.AddAsync(new PrescriptionMedicament()
            {
                IdMedicament = medicament.IdMedicament,
                IdPrescription = prescription.IdPrescription,
                Dose = medicament.Dose,
                Details = medicament.Description
            });
        }
        await _context.SaveChangesAsync();
    }

    public async Task<PatientDataDto> GetPatientData(int patientId)
    {
        
        var patient = await _context.Patients.FirstOrDefaultAsync(p => p.IdPatient == patientId);
        if (patient == null)
        {
            throw new Exception($"Patient with id {patientId} not found");
        }
        var patientDataDto = new PatientDataDto
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Birthdate = patient.Birthdate
        };
        var prescriptionsDto = new List<PrescriptionDataDto>();

        var prescriptions = await _context.Prescriptions.Where(p => p.IdPatient == patientId).ToListAsync();
        foreach (var prescription in prescriptions)
        {
            var prescriptionDataDto = new PrescriptionDataDto();
            prescriptionDataDto.IdPrescription = prescription.IdPrescription;
            prescriptionDataDto.Date = prescription.Date;
            prescriptionDataDto.DueDate = prescription.DueDate;

            var prescriptionMedicamentsObj = await _context.PrescriptionMedicaments.Where(pm => pm.IdPrescription == prescription.IdPrescription).ToListAsync();
            var medicaments = new List<MedicamentDataDto>();
            foreach (var prescriptionMedicament in prescriptionMedicamentsObj)
            {
                var medicamentDataDto = new MedicamentDataDto
                {
                    IdMedicament = prescriptionMedicament.IdMedicament
                };

                var medicamentObj = await _context.Medicaments.FirstAsync(m => m.IdMedicament == prescriptionMedicament.IdMedicament);
                medicamentDataDto.Name = medicamentObj.Name;
                
                medicamentDataDto.Dose = prescriptionMedicament.Dose;
                medicamentDataDto.Description = prescriptionMedicament.Details;
                
                medicaments.Add(medicamentDataDto);
            }
            prescriptionDataDto.Medicaments = medicaments;

            var doctorObj = await _context.Doctors.FirstAsync(doctor => doctor.IdDoctor == prescription.IdDoctor);
            var doctorDataDto = new DoctorDataDto
            {
                IdDoctor = doctorObj.IdDoctor,
                FirstName = doctorObj.FirstName
            };
            prescriptionDataDto.Doctor = doctorDataDto;
            
            prescriptionsDto.Add(prescriptionDataDto);
            
        }
        
        patientDataDto.Prescriptions = prescriptionsDto.OrderBy(p => p.DueDate).ToList();
        
        return patientDataDto;
    }
}
