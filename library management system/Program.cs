/*
   Title : Library Management System
   Author : Kalaivani G
   Created At : 18-10-2022
   Updated At : 1-11-2022
   Reviewed By : 
   Reviewed At : 

   */

using System;
using System.Data.SqlClient;
using System.Data;
#nullable disable
namespace SampleProgram
{
   public class User
    {
        public string userName,userID;
        public string  emailid, userpassword, loginname;
        
        public DateOnly dateofbirth;
        public long mobileNumber;
        private  int numberOfBookBorrowed; 
        public int borrowedBookRate{get{return numberOfBookBorrowed;} set{numberOfBookBorrowed=value;}}
        public  int numberOfBookReturned; 
        public bool validate=false;
       
            public String constring=@"Data Source=DESKTOP-A5J6BP7\SQLEXPRESS ;Initial Catalog=library ; User ID=kalai ; Password=kalai@782001";
            public void register()
            {
             
             Console.WriteLine("\n ********** REGISTER *********\n");

             SqlConnection conn=new SqlConnection(constring);
             conn.Open();
             Console.Write("ENTER YOUR NAME :");
             userName= Console.ReadLine();
             Console.Write("ENTER YOUR DATE OF BIRTH: ");
             dateofbirth=DateOnly.Parse(Console.ReadLine());
             Console.Write("ENTER YOUR EMAIL ID: ");
             emailid= Console.ReadLine();
             Console.Write("ENTER YOUR PASSWORD: ");
             userpassword= Console.ReadLine();
             Console.Write("ENTER YOUR mobilenumber: ");
             mobileNumber=long.Parse(Console.ReadLine());
             userID=String.Concat(userName,dateofbirth.ToString());
              
             String query="insert into userdetails(username,dateofbirth,emailid,userpassword,mobileNumber) values('"+userName+"','"+dateofbirth+"','"+emailid+"','"+userpassword+"','"+mobileNumber+"')";
             SqlCommand cmd=new SqlCommand(query,conn);
             cmd.ExecuteReader();
             Console.WriteLine("\n REGISTERED SUCCESSFULLY!!!...");

             Console.WriteLine("\n ********** LOGIN *********");
             login();
            }
       
            public void login()
            {
                DataTable datatable=new DataTable();
                SqlConnection conn=new SqlConnection(constring);
                conn.Open();

                Console.Write("ENTER YOUR NAME :");
                loginname= Console.ReadLine();
                Console.Write("ENTER YOUR PASSWORD :");
                String userpassword= Console.ReadLine();
                
                SqlDataAdapter adapter=new SqlDataAdapter("Select * from userdetails",conn);
                adapter.Fill(datatable);
                
            
                foreach(DataRow row in datatable.Rows)
                {
                    
                    if((loginname==row["userName"].ToString() && userpassword==row["userpassword"].ToString()))
                    {
                        validate=true;
                        break;
                    }
                }
             
            
        }
        
        
    }
    public class Book : User
    {
        
        public List<Book> booklist=new List<Book>();
        public  String bookName;
        public DateTime borrowedDate,returnDate;
        private String fineAmount;
        public bool loggedin=true;
        
        public void verify()
        {

          if(validate==false)
             {
                Console.WriteLine("please register");
                Console.WriteLine(userID);
                loggedin=false;
                register();
             }
             else
             {
                Console.WriteLine("alreday registered");
                loggedin=true;

             }
        }
        
        
        // Add borrowed book details to list

        internal protected void  addBook()
        {
          Console.Write("enter bookname:");
          bookName=Console.ReadLine();
          Random rnd=new Random();
          Console.WriteLine($"ISBN : {rnd.Next()}");
          Console.Write("enter borrowedDate:");
         
          borrowedDate=DateTime.Parse(Console.ReadLine()!);
          booklist.Add(new Book{
            bookName=bookName,
            borrowedDate=borrowedDate,
            returnDate=returnDate,
            fineAmount=fineAmount});
          Console.WriteLine($"{bookName} is borrowed");
          borrowedBookRate=booklist.Count;
          
        }

        // Update book details once returned 
        //if book is returned after the targeted due date means the fine amount is generated

        public void returnbook()
        {
            numberOfBookReturned++;
            String returnBook;
            Console.Write("enter bookname to be returned : ");
            returnBook=Console.ReadLine()!;
            Console.Write("enter book returned date: ");
            
            returnDate=DateTime.Parse(Console.ReadLine()!);
            int dateOfReturn=int.Parse((returnDate-borrowedDate).ToString("dd"));
            //book.fineAmount=fineAmount;
            foreach(Book book in booklist)
            {
                if(book.bookName==returnBook)
                {
                    book.returnDate=returnDate;
                if((dateOfReturn >0)&&(dateOfReturn < 7))
                {
                    fineAmount="-";
                    
                    break;
                }
                else if((dateOfReturn < 14) && (dateOfReturn >=7))
                {
                    fineAmount="10";
                    break;
                }
                else if((dateOfReturn < 21) && (dateOfReturn >=14))
                {
                    fineAmount="20";
                    break;
                }
                else if((dateOfReturn < 21) && (dateOfReturn >=32))
                {
                    fineAmount="50";
                    break;
                }
                else
                {
                    fineAmount="100";
                    break;
                }
                }
            }
        }
    
    // Displays all borrowd and returned book details of user.

        public void displaydetails()
        {
            
            Console.WriteLine("\n************** PROFILE **************\n");
            Console.WriteLine($"USER ID : {userID}");
            Console.WriteLine("Number Of Books Borrowed: {0}",borrowedBookRate);
            Console.WriteLine("Number Of Books Returned: {0}",numberOfBookReturned);
            int count=1; 
            Console.WriteLine("\n**************************************************\n");
            Console.WriteLine("-----------USER BOOKDETAILS---------\n");
            foreach(var book in booklist)
            {
               
              Console.WriteLine($"{count} . [BookName : {book.bookName}]  [Borroweddate : {book.borrowedDate}] [returndate : {book.returnDate}] [fineamount : {book.fineAmount}]");
              count++;
            }
            
        }


       public  void logout() 
       {
          Console.WriteLine("\n******* logged out successfully !.. ********\n\n HAPPY LEARNING ");

       }
      
    }

     
    class Database : Book
    {
       
         char continueoption;

         // invokes all the methods in base class according to the conditions and displays it

        public void display()
        {     
           
            if(loggedin==true)
            {
             Console.WriteLine("---BOOKLIST----");
             Console.WriteLine($"\nWelcome {userName} to our library \n");
             Console.WriteLine("\nPLEASE ENTER \n1.ADD BOOK TO LIST \n 2.RETURN BOOK \n 3.DISPLAY BOOK LIST");
            do{
               
            int option=int.Parse(Console.ReadLine()!);
            switch(option)
            {
                case 1:
                      
                       addBook();
                       break;
                case 2:
                       returnbook();
                       break;
                case 3:
                        displaydetails();
                        break;
                default:
                      Console.Write("ENTER PROPER OPTION..");
                      break;
                     
            }
            Console.Write("\nWANTS TO CONTINUE... PRESS y OR n ");

            continueoption=char.Parse(Console.ReadLine()!);
            
            }while(continueoption=='y');
            logout();
            }
           
           
        }

    }

    class LibraryMangementSystem
    {
        static void Main()
        {
            Database database=new Database();
            Book book=new Book();
            book.login();
            book.verify();
            database.display();
           
        }
    }

}












