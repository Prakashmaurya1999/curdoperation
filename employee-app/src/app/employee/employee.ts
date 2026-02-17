import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { EmployeeService } from '../employee-service/employee-service';

@Component({
  selector: 'app-employee',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './employee.html',
  styleUrl: './employee.css'
})
export class Employee implements OnInit {

  employeeForm!: FormGroup;
  employees: any[] = [];

  constructor(private fb: FormBuilder,
              private service: EmployeeService) {}

  ngOnInit() {
    this.employeeForm = this.fb.group({
      id: [0],
      name: [''],
      email: [''],
      mobile: [''],
      age: [''],
      salary: [''],
      status: [true]
    });

    this.loadEmployees();
  }

  loadEmployees() {
    this.service.getEmployees().subscribe(res => {
      this.employees = res;
    });
  }

  save() {
    console.log("employeeForm",this.employeeForm);
    if (this.employeeForm.value.id == 0) {
      this.service.addEmployee(this.employeeForm.value)
        .subscribe(() => {
          this.loadEmployees();
          this.employeeForm.reset({ id: 0, status: true });
        });
    } else {
      this.service.updateEmployee(
        this.employeeForm.value.id,
        this.employeeForm.value
      ).subscribe(() => {
        this.loadEmployees();
        this.employeeForm.reset({ id: 0, status: true });
      });
    }
  }

  edit(emp: any) {
    this.employeeForm.patchValue(emp);
  }

  delete(id: number) {
    this.service.deleteEmployee(id)
      .subscribe(() => this.loadEmployees());
  }
}
