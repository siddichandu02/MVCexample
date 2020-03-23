using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Ecercise2_LINQ.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Ecercise2_LINQ.Controllers
{
    public class FriendController : Controller
    {
        readonly string ConnectionInformation = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=adocrud;Integrated Security=true";
        SqlDataReader myReader;       
        private readonly Data _db;
        public FriendController(Data db)
        {
           this. _db = db;
        }
        public IActionResult InsertNewFriend()
        {
            return View();
        }
        public IActionResult Index()
        {
            var friend = _db.friends.ToArray();
            return View();
        }
        [HttpPost,Route("Post")]
        // public IActionResult Post(String FriendId, String FriendName, String Place)
        public IActionResult Post(Friend friend)

        {/*ADO
            Friend friend = new Friend();
            friend.FriendId =long.Parse( FriendId);
            friend.FriendName = FriendName;
            friend.Place = Place;

            SqlConnection MainConnection = new SqlConnection(ConnectionInformation);
            MainConnection.Open();
            string MyCommand = "Insert Into Friends(FriendId,FriendName,Place) values("+friend.FriendId+",'"+friend.FriendName+"','"+friend.Place+"')";
            SqlCommand myCommand = new SqlCommand(MyCommand,MainConnection);
            myCommand.ExecuteNonQuery();

            MainConnection.Close();
           


           return View();*/

            //   Friend friend = new Friend();
            // friend.FriendId = long.Parse(FriendId);
            //friend.FriendName = FriendName;
            //friend.Place = Place;
            _db.friends.Add(friend);
            _db.SaveChanges();
            return View();
        }

        //update function
        [HttpPost,Route("Update")]
        public IActionResult Get()
        {/*ADO
            Friend friend = new Friend();
            friend.FriendId = long.Parse(Request.Form["FriendId"]);
            friend.FriendName = Request.Form["FriendName"];
            friend.Place = Request.Form["Place"];

            SqlConnection MainConnection = new SqlConnection(ConnectionInformation);
            MainConnection.Open();
            
            string MyCommand = "Update Friends SET FriendName='" + friend.FriendName + "',Place='" + friend.Place + "' where FriendId=" + friend.FriendId;

            SqlCommand myCommand = new SqlCommand(MyCommand, MainConnection);
            myCommand.ExecuteNonQuery();

            MainConnection.Close();*/
            
           
           
            var id = int.Parse(Request.Form["FriendId"]);
            var update = _db.friends.SingleOrDefault(x => x.FriendId == id);
            if (update != null)
            {
                update.FriendName = Request.Form["FriendName"];
                update.Place = Request.Form["Place"];
                _db.SaveChanges();



            }
            return View();




            
        }
        [Route("select")]
        public IActionResult select()
        {
            /*ADO
            
            SqlConnection MainConnection = new SqlConnection(ConnectionInformation);
            MainConnection.Open();
            string MyCommand = "select * from Friends";
            SqlCommand myCommand = new SqlCommand(MyCommand, MainConnection);
            myReader=  myCommand.ExecuteReader();
            //SqlDataAdapter adp = new SqlDataAdapter(MyCommand, MainConnection);
            //DataTable dt = new DataTable();
            // adp.Fill(dt);
            var model = new List<Friend>();
            while (myReader.Read())
           {
                Friend friends = new Friend();
                friends.FriendId = Int32.Parse(myReader.GetValue(0).ToString());
                friends.FriendName = myReader.GetValue(1).ToString();
                friends.Place = myReader.GetValue(2).ToString();
                model.Add(friends);
            }

            MainConnection.Close();
            return View(model);*/
            
            var rows = _db.friends.ToArray();
            return View(rows);

        }
     
       
        [HttpPost , Route("delete")]
        public void Delete()
        {/*ADO

            var id = int.Parse(Request.Form["FriendId"]);
            SqlConnection MainConnection = new SqlConnection(ConnectionInformation);
            MainConnection.Open();
            string MyCommand = "delete from Friends where FriendId = " + id;
            SqlCommand myCommand = new SqlCommand(MyCommand, MainConnection);
            myCommand.ExecuteNonQuery();

            MainConnection.Close();*/
            
            var id = int.Parse(Request.Form["FriendId"]);
            var delete = _db.friends.SingleOrDefault(x => x.FriendId == id);
            if (delete!=null){
                _db.friends.Remove(delete);
                _db.SaveChanges();
               
            }
            
        }
 
    }
}