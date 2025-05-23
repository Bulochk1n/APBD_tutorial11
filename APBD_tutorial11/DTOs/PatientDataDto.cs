namespace APBD_tutorial11.DTOs;

public class PatientDataDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
    public List<PrescriptionDataDto> Prescriptions { get; set; }
}

public class PrescriptionDataDto
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public List<MedicamentDataDto> Medicaments { get; set; }
    public DoctorDataDto Doctor { get; set; }
}

public class DoctorDataDto
{
    public int IdDoctor { get; set; }
    public string FirstName { get; set; }
}

public class MedicamentDataDto
{
    public int IdMedicament { get; set; }
    public string Name { get; set; }
    public int Dose { get; set; }
    public string Description { get; set;  }
}