using System;
using System.Runtime.Intrinsics.X86;

namespace Example;
public interface IUser
{
    string Id { get; set; }
    string UserName { get; set; }
    string Password { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    bool IsActive { get; set; }
}
public interface IPersonal : IUser
{
    
    string SSN { get; set; }
    decimal Salary { get; set; }
    void CalculateSalary(short workingHours);
}

public interface IStudent : IUser
{
    string StudentNo { get; set; }
    int Absenteeism { get; set; }
    byte Marks { get; set; }
}
public interface IJobber:IUser
{
    string PlateNo { get; set; }    
    string WorkArea { get; set; }
}

public abstract class User:IUser
{
    public string Id { get; set; }  
    public string UserName { get; set; }
    public string Password { get; set; }
    public bool IsActive { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public User() { }
    public User(string userName, string password,bool isActive) {
        UserName = userName;
        Password = password;
        IsActive = isActive;
    }

}

public class Personal : User, IPersonal
{
    public string SSN { get; set; }
    public decimal Salary { get; set; }
    public Personal() { }
    public Personal(string userName, string password, bool isActive, string ssn, decimal salary) : base(userName, password, isActive)
    {
        SSN = ssn;
        Salary = salary;
    }
    public void CalculateSalary(short workingHours)
    {
        Console.WriteLine(@$"Calculated salary: {Salary * workingHours} ₺");
    }
}


public class Student:User,IStudent {
    
    public string StudentNo {  get; set; }
    public int Absenteeism { get; set; }
    public byte Marks { get; set; }
    public Student() { }
    public Student(string userName, string password, bool isActive, int absenteeism, byte marks,string studentNo) : base(userName, password, isActive)
    {
        Absenteeism = absenteeism;
        Marks = marks;
        StudentNo = studentNo;

    }
}
public class Jobber:User,IJobber
{
    public string PlateNo { get; set; }
    public string WorkArea { get; set; }
    public Jobber() { }
    public Jobber(string userName, string password, bool isActive, string workArea,string plateNo) : base(userName, password, isActive)
    {
        WorkArea = workArea;
        PlateNo = plateNo;

    }
}
