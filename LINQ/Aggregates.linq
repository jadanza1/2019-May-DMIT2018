<Query Kind="Expression">
  <Connection>
    <ID>c1a07453-b2f4-488e-9263-4e75afa78a5c</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//Aggregates

//.Count(), .Sum(), .Min(), .Max(), .Average()
// NOTE: Aggregate works against datasets 


//List all albums showing the album title, album artist name,
// and the number of tracks on the album.

//This solution will demonstrate a using a combination of Query Syntax and Method Syntax.


//from x in Albums
//select new 
//{
//	title = x.Title,
//	artist = x.Artist.Name,
//	trackCount = x.Tracks.Count()
//}

//from x in Albums
//select new 
//{
//	title = x.Title,
//	artist = x.Artist.Name,
//	trackCount = (from y in x.Tracks 
//					select y).Count()
//}

//from x in Albums
//select new
//{
//	title = x.Title,
//	Artist = x.Artist.Name,
//	trackCount = (from y in Tracks 
//					where y.AlbumId == x.AlbumId
//					select y).Count()
//}

// List the artist and their number of albums
// order the list from most albums by an Artist to least albums by an Artist.

from x in Artists
orderby x.Albums.Count() descending,
	x.Name ascending
select new
{
	name = x.Name,
	albumCount = x.Albums.Count()
};

//Find the Maximum number of Albums for All artist

(from x in Artists
select x.Albums.Count()).Max();
//OR

(Artist.Select(x => x.Albums.Count())).Max();

//product a list of albums which have tracks showing their
//title, artist name, number of tracks on album and total price of
// all the tracks for that album

//from x in Albums
//where.Tracks.Count() > 0
//select new
//{
//	title = x.Title,
//	name = x.Artist.Name,
//	trackCount = x.Tracks.Count(),
//	albumCost = x.Tracks.Sum(tr => tr.UnitPrice) 
//	
//	
//}

//from x in Albums
//where x.Tracks.Count() > 0
//select new
//{
//	title = x.Title,
//	name = x.Artist.Name,
//	trackCount = (from y in x.Tracks
//					select y).Count(),
//	albumCost = (from y in Tracks 
//					where y.AlbumId == x.AlbumId
//					select y.UnitPrice).Sum() 
//}


//List all the playlist which have a track showing the playlist name,
// numbers of tracks of the playlist, the cost of the playlist, and
// the total storage size of the playlist in megabytes.





