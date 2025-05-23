using APBD_tutorial11.DTOs;
using APBD_tutorial11.Models;

namespace APBD_tutorial11.Services;

public interface IDbService
{
    Task IssueNewPrescription(PrescriptionDto prescriptionDto);
    Task<PatientDataDto> GetPatientData(int patientId);
}