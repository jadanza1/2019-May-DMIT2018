<Query Kind="Statements">
  <Connection>
    <ID>5b3e2a72-84b0-4f63-a42f-6916eac3f401</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

//JOIN 3 table
from artistSide in Artists
join  albumSide in Albums
on artistSide.ArtistId equals albumSide.ArtistId
join trackSide in Tracks
on albumSide.AlbumId equals trackSide.AlbumId
select new
{
	Artist = artistSide.Name,
	Album = albumSide.Artist,
	GenreId = trackSide.AlbumId
}


//list the artists and their number of albums
// order the list from most albums by an artist to least albums by an artist

var q1 = from x in Artists 
orderby (from y in Albums where y.ArtistId == x.ArtistId select y).Count() descending
select new{
	name = x.Name,
	album = (from y in Albums where y.ArtistId == x.ArtistId select y).Count()
};

//find the maximum number of albums for all artist

var q2 = (from x in Artists
			select x.Albums.Count()).Max();
//produce a list of albums which have tracks showing their
//title, artist name, number of tracks on album and total price of
// all tracks for that album			
var q3 = (from x in Albums
			where x.Tracks.Count() > 0 
			select new {
				Title = x.Title,
				Artist = x.Artist.Name,
				Track = (from y in Tracks 
							where y.AlbumId.Equals(x.AlbumId)
							select y).Count(),
				TotalPrice = (from y in Tracks
								where y.AlbumId.Equals(x.AlbumId)
								select y.UnitPrice).Sum()
			});
q3.Dump();

	// Nested Query
	// a query within another query
	
	//List all sales support employees showing their fullname (lastname, firstname),
	//  their title and the number of customers each supports. Order by fullname.
	//  In addition show a list of the customers for each employee. For the customer
	//  list, show the fullname and phone number.

var q4 = (from x in Employees
			where x.Title.Contains("support")
			orderby x.LastName, x.FirstName
			select new
			{
				EmpName = x.LastName + "," + x.FirstName,
				EmpTitle = x.Title,
				CustomerCount = (from y in Customers 
									where y.SupportRepId.Equals(x.EmployeeId)
									select y).Count(),
				CustomerList = (from y in Customers
									where y.SupportRepId.Equals(x.EmployeeId)
									select new
									{
										CustomerName = y.LastName + "," + y.FirstName,
										CustomerPhone = y.Phone
									})
			});
			
q4.Dump();
		
	//create a list of albums showing the album title and artist name.
	//show albums with 25 or more tracks only.
	//show the songs on the album (name and length in minutes and seconds)
	//create the DTO and POCO classes

var q5 = (from x in Albums
			where x.Tracks.Count() > 25
			select new
			{
				AlbumTitle = x.Title,
				artist = x.Artist.Name,
				song = (from y in Tracks
						select new
						{
							TrackTitle = y.Name,
							Length = y.Milliseconds/60000 + ":" + (y.Milliseconds%60000)/1000
							
							
						})
			});
q5.Dump();		

// create an alphabetic list of Albums by ReleaseLabel
// Albums Missing labels will be listed as "Unknown"
var q6 = (from x in Albums
			orderby x.ReleaseLabel
			select new{
				title = x.Title,
				label = x.ReleaseLabel == null ? "Unknown" : x.ReleaseLabel
			});
			
q6.Dump();

// create an Alphabetic list of Albums by Decades
// 70s, 80s, 90s

var q6 = (from x in Albums
			where x.ReleaseYear >= 1970
			orderby x.Title,x.ReleaseYear 
			select new{
					Title = x.Title,
					ReleaseYear = (x.ReleaseYear < 1979 && x.ReleaseYear >= 1970 ? "70's" : 
									x.ReleaseYear < 1989 && x.ReleaseYear >= 1980 ? "80's" :
									x.ReleaseYear < 1999 && x.ReleaseYear >= 1990 ? "90's" : "Modern")
			});
q6.Dump();


	//Create a list whether a particular track length 
	// is greater than, less than or the average teack length
	
	//PROBLEM: I need the average track length before testing 
	// the individual track lenght against the average
	
var q7_result_Avg = (from x in Tracks
						select x.Milliseconds).Average();
var q7 = (from x in Tracks
//			orderby x.Name
			select new{
				name = x.Name,
				length_size = (x.Milliseconds > q7_result_Avg ? "Long" :
								x.Milliseconds < q7_result_Avg ? "Short" : "Average")
			});
q7.Dump();

// Report Albums by ReleaseYear
var q8 = (from x in Albums
			group x by x.ReleaseYear into gTemp
				select gTemp
			);
q8.Dump();

//report albums by ReleaseYear showing the year and number of
// albums for that year. Order by the most recent year
var q9 = (from x in Albums
			group x by x.ReleaseYear into gTemp
			orderby gTemp.Count(), gTemp.Key 
			select new
			{
				Year = gTemp.Key,
				AlbumCount = gTemp.Count(),
			});
q9.Dump();

//Report albums by ReleaseYear showing the year and number of
// albums for that year. Order by year with most albums then by year.
// Report the album title, artist name and number of albums tracks
// for each album in each year

var q10 = (from x in Albums
			group x by x.ReleaseYear into gTemp
			orderby gTemp.Count() descending ,gTemp.Key ascending
			select new{
				year = gTemp.Key,
				count = gTemp.Count(),
				albums = from y in gTemp
							where y.ReleaseYear == gTemp.Key
							select new {
								Title = y.Title,
								Artist = y.Artist.Name,
								NumberOfTracks = (from p in y.Tracks
													select p).Count()
							}
			});
q10.Dump();

//grouping can be done on entity attributes determine using a 
//	navigational property. List tracks for Albums produced after 2010 by Genre Name.

var q11 = (from x in Tracks
			where x.Album.ReleaseYear > 2010
			group x by x.Genre.Name into gTemp
			select new{
				genre = gTemp.Key,
				count = gTemp.Count()
			});
q11.Dump();

//create a list of customers by employee support individual
//showing the employee lastname, firstname (phone) , the number
// of customers for this employee, and a customer list for the 
// employee by state, city, and customer firstname + last name

var q12 = (from x in Customers
			group x by x.SupportRepIdEmployee into gTemp
			select new
			{
				empInfo= gTemp.Key.LastName + "," + gTemp.Key.FirstName + "("+ gTemp.Key.Phone +")",
				cusCount = (from y in Customers
							where y.SupportRepIdEmployee == gTemp.Key
							select y).Count(),
				cusList = (from z in Customers
							where z.SupportRepIdEmployee == gTemp.Key
							select new{
								cusName = z.LastName + "," + z.FirstName,
								cusState = z.State,
								cusCity = z.City,
							})
				
			});
			
q12.Dump();

//produce a list of albums which have tracks showing their
//title, artist name, number of tracks on album and total price of
// all tracks for that album	
var q13 = (from x in Albums
			where x.Tracks.Count() > 0
			select new
			{
				title = x.Title,
				name = x.Artist.Name,
				trackNumber = (from y in Tracks
								where y.AlbumId == x.AlbumId
								select y).Count(),
				trackPrice = (from y in Tracks
								where y.AlbumId.Equals(x.AlbumId)
								select y.UnitPrice).Sum()
			});
q13.Dump();

//find the maximum number of albums for all artist

var q14 = (from x in Artists
			select x.Albums.Count()).Min();
			
q14.Dump();