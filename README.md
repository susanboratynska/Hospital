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
- Patient E-cards - (Patient & Visitors)  
- Events Calendar - Home Page (Main Nav)  
- MVP (Nav, Pages, Master Page)  

##### Het Kansara  
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
- Doctor’s Directory  
- A Library of Health Information  
- FAQ  

##### Mounica Sykam  
- Donation  
- Job Portals  
- Contact Us  

##### N01375163: Hilmi Yildirim  
- List of Programs & Services  
- Testimonials (About Us)  
- Emergency page (Separate button / Patients & Visitors)  

  
