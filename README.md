# Hospital Redesign Project  
  
**_Team 2 / North Bay Regional Health Centre_**  
  
## :zap: Project Description  
  
The purpose is to design and build a hospital website in order both for the visitors and the admins to take advantage of it most effectively and efficiently. Building a user-friendly and accessible website using a customized CMS will make the admins' tasks much easier and the visitors' experience beneficial.  
  
The software is developed with Visual Studio in .NET (HTML, CSS, C#, MySQL).The instructions to run the project are as follows:  
  
### How to run the project  
  
1. Clone or download the repository  
2. Create a folder named "App_Data"  
3. Open the project in Visual Studio  
4. Build the project: `Build -> Build Solution`  
5. Command "Enable-Migrations" in package manager console  
6. Command "Add-Migration initial" in package manager console  
7. Command "Update-Database" in package manager console    
8. Run the home page view  
  
## :bookmark_tabs: Documentation  
  
Please refer to "Documentation" folder to see the following reports:  
- **Wireframes**: The blueprints of the pages grouped by the website components/features.  
- **Narratives**: Explanations of the features by use case stories. 
- **Entity Relationship Diagrams**: All database structure (tables & relations) can be seen as visualized.  
- **Code Content**: MVC structure of the project: Models, Views & Controllers.  
  
### Teamwork Report
  
The team members and their contributions are below.  
Individual works can be seen in the sub-folders in "Documentation".  
  
##### N01371157: Susan Boratynska  
- Please note: HospitalCampuses entries must be completed first before Patient eCard and Events features can be tested as they rely on campus data.

- Hospital Campus (Admin)
  - Models: Models/HospitalCampus.cs
  - Controllers: Controllers/HospitalCampusController.cs 
  - Views:
    - Views/HospitalCampus/Add/.cshtml
    - Views/HospitalCampus/List/.cshtml
    - Views/HospitalCampus/Show/.cshtml
    - Views/HospitalCampus/Update.cshtml

- Patient eCards - (Public & Admin Views)
  - Models: Models/PatientEcard.cs
  - Controllers: Controllers/PatientEcardController.cs
  - Views:
    - Views/PatientEcard/Add.cshtml
    - Views/PatientEcard/Confirm.cshtml
    - Views/PatientEcard/List.cshtml
    - Views/PatientEcard/Show.cshtml
      - ViewModels:  
        - Models/ViewModels/ListPatientEcards.cs

- Events Calendar (Admin)
  - Models/PatientEcard.cs
    - Models/Event.cs
    - Models/ViewModels/AddEvent.cs
    - Models/ViewModels/ListEvents.cs
   - Controllers: Controllers/EventController.cs
   - Views:
     - Views/Events/Add.cshtml
     - Views/Events/List.cshtml
     - Views/Events/Show.cshtml
     - Views/Events/Update.cshtml
   
- Additional Contributions
    - Views:
      - Views/Home/Index
      - Views/Home/About
      - Views/Home/Contact


##### N01330009: Het Kansara  
- Parking Registration  
  - Models
    - ViewModels
      1. ParkingUpdate.cs
    1. Parking.cs
    2. ParkingBooking.cs
  - Controllers
    1. ParkingController.cs
  - Views
    1. Parking/book.cshtml
    2. Parking/Bookings.cshtml
    3. Parking/Delete.cshtml
    4. Parking/UpdateBooking.cshtml
- Doctor's Appointment Booking Form  
  - Models
    - ViewModels
      1. DoctorsAppointmentUpdate.cs
    1. Doctor.cs
    2. DoctorAppointment.cs
  - Controllers
    1. DoctorAppointment.cs
  - Views
    1. DoctorAppointment/Add.cshtml
    2. DoctorAppointment/Bookings.cshtml
    3. DoctorAppointment/Delete.cshtml
    4. DoctorAppointment/InvalidUser.cshtml
    5. DoctorAppointment/MyBookings.cshtml
    6. DoctorAppointment/Update.cshtml
- Articles and News Manager  
    
##### Maitri Modi  
- Media Publications  
- Volunteers’s application/Postings  
- Online Bill Payment   

##### Michael Pavlovic 
- FAQ
  - Models
    1. Models/Faq.cs
  - Controllers
    1. Controllers/FaqController.cs
  - Views
    1. Views/Faq/Add.cshtml
    2. Views/Faq/List.cshtml
    3. Views/Faq/Public.cshtml
    4. Views/Faq/Show.cshtml
    5. Views/Faq/Update.cshtml
- A Library of Health Information
  - Models
    1. Models/HealthLibrary.cs
  - Controllers
    1. Controllers/HealthLibraryController.cs
  - Views
    1. Views/HealthLibrary/Add.cshtml
    2. Views/HealthLibrary/List.cshtml
    3. Views/HealthLibrary/Public.cshtml
    4. Views/HealthLibrary/PublicShow.cshtml
    5. Views/HealthLibrary/Show.cshtml
    6. Views/HealthLibrary/Update.cshtml
- Doctor’s Directory  

##### N01363730 - Mounica Sykam  
- Donation  
  - Models
    1. Donation.cs
  - Controllers
    1. DonationController.cs
  - Views
    1. Donation/Add.cshtml
    2. Donation/DonationSuccess.cshtml
    3. Donation/List.cshtml
    4. Donation/View.cshtml
- Job Portals  
  - Models
    - ViewModels
      1. UpdateJob.cs
    1. Job.cs
    2. JobType.cs
  - Controllers
    1. JobController.cs
  - Views
    1. Job/Add.cshtml
    2. Job/Apply.cshtml
    3. Job/careersListing.cshtml
    4. Job/Delete.cshtml
    5. Job/JobPosts.cshtml
    6. Job/JobsListing.cshtml
    7. Job/Update.cshtml
- Contact Us  

##### N01375163: Hilmi Yildirim  
- List of Programs & Services  
- Testimonials (About Us)  
- Emergency page (Separate button / Patients & Visitors)  

  
