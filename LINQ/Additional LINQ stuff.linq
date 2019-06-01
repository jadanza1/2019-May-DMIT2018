<Query Kind="Statements">
  <Connection>
    <ID>55e25851-44fd-4e25-a5d5-fdf4b5e5e460</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

//UNION

//To get both the albums with tracks and without tracks you can use a .Union()
//In a union you need to ensure cast typing is correct and columns cast types match identically
//Each query needs to have the same number of columns, in the same order
//Since Average returns a double, the division in the first part of the union needs
//   to be done as Double, therefore the value 1000 (int) is properly set up as a double.
//Note the sorting is as method syntax on the Union
//
// (query1).Union(query2).Union(queryn).OrderBy(first sort).ThenBy(nth sort)

var unionresults = (from x in Albums
					where x.Tracks.Count() > 0
					select new{
						Title = x.Title,
						TotalTracksforAlbum = x.Tracks.Count(),
						TotalPriceForalbumtracks = x.Tracks.Sum(y => y.UnitPrice),
						AverageTrackLengthA = x.Tracks.Average(y => y.Milliseconds)/1000.0,
						AverageTrackLengthB = x.Tracks.Average(y => y.Milliseconds/1000.0)
						}
					).Union(
						from x in Albums
						where x.Tracks.Count() == 0
						select new{
								Title = x.Title,
								TotalTracksforAlbum = 0,
								TotalPriceForalbumtracks = 0.00m,
								AverageTrackLengthA = 0.00,
								AverageTrackLengthB = 0.00
							}
					).OrderBy(y => y.TotalTracksforAlbum).ThenBy(y => y.Title);
unionresults.Dump();


//boolean filters .All() or .ANY()

// .Any() method iterates throught the entire collection to see if
//     any of the items match the specified condition
// returns a true or false NO DATA
// an instance of the Collection that receives a true on the condition is
//     selected for processing

Genres.OrderBy(x => x.Name).Dump();
//Show Genres that have tracks which are not on any playlist.
var genretrack =from x in Genres
						where x.Tracks.Any(tr => tr.PlaylistTracks.Count() == 0)
						orderby x.Name
						select new
						{
							name = x.Name
						};
genretrack.Dump();


// .All() method iterates throught the entire collection to see if
//     all of the items match the specified condition
// returns a true or false NO DATA
// an instance of the Collection that receives a true on the condition is
//     selected for processing

//Show Genres that have all their tracks appearing at least once on a playlist.
var populargenres =from x in Genres
						where x.Tracks.All(tr => tr.PlaylistTracks.Count() > 0)
						orderby x.Name
						select new
						{
							name = x.Name,
							thetracks = (from y in x.Tracks
										 where y.PlaylistTracks.Count() > 0
										 select new
										{
											
											song = y.Name,
											count =  y.PlaylistTracks.Count()
										})
						};
populargenres.Dump();

//Sometimes you have two list that need to be compared
//Usually you are looking for items that are the same (in both collections) OR
//        you are looking for items that are different
//In either case: you are comparing one collection to a second collection

//obtain a distinct list of all playlist tracks for Roberto Almeida (username AlmeidaR)
var almeida = (from x in PlaylistTracks
where x.Playlist.UserName.Contains("Almeida")
orderby x.Track.Name
select new
{
	genre = x.Track.Genre.Name,
	Id = x.TrackId,
	Song = x.Track.Name
}).Distinct();

//obtain a distinct list of all playlist tracks for Michelle Brooks (username BrooksM)
var brooks =(from x in PlaylistTracks
where x.Playlist.UserName.Contains("Brooks")
orderby x.Track.Name
select new
{
	genre = x.Track.Genre.Name,
	Id = x.TrackId,
	Song = x.Track.Name
}).Distinct();

//list the tracks that both Roberto and Michelle like
var likes = almeida.Where (a => brooks.Any(b => b.Id == a.Id)).OrderBy(a => a.genre).Select(a => a);
			
likes.Dump();

//list the Roberto's tracks that Michelle does not have.
var almeidadif = almeida.Where (a => !brooks.Any(b => b.Id == a.Id)).OrderBy(a => a.genre).Select(a => a);
			
almeidadif.Dump();

//list the Michelle's tracks that Roberto does not have.
var brooksdif = brooks.Where (b => !almeida.Any(a => a.Id == b.Id)).OrderBy(a => a.genre).Select(b => b);
			
brooksdif.Dump();

//notice that the R&B Soul for Roberto is 1426 and Michelle is 1432