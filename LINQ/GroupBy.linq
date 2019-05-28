<Query Kind="Expression">
  <Connection>
    <ID>c1a07453-b2f4-488e-9263-4e75afa78a5c</ID>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//Grouping of Data within itself 
// a) by a column
// b) by a multiple columns
// c) by an entity

//grouping can be saved temporarily into 
// a dataset and that dataset can
// further processed for reporting

//If you group into a temporary dataset
// any further processing MUST be done
// against the temporary Dataset

// Report Albums by ReleaseYear
from x in Albums
group x by x.ReleaseYear into gyear
select gyear


//report albums by ReleaseYear showing the year and number of
// albums for that year. Order by the most recent year

from x in Albums
group x by x.ReleaseYear into gyear
orderby gyear.Key descending
select new
{
	year = gyear.Key,
	albumCount = gyear.Count()
}

//Another Example

from x in Albums
group x by x.ReleaseYear into gyear
orderby gyear.Count() descending, gyear.Key ascending
select new
{
	year = gyear.Key,
	albumCount = gyear.Count()
}


//Report albums by ReleaseYear showing the year and number of
// albums for that year. Order by year with most albums then by year.
// Report the album title, artist name and number of albums tracks
// for each album in each year

from x in Albums
group x by x.ReleaseYear into gyear
orderby gyear.Count() descending, gyear.Key ascending
select new
{
	year = gyear.Key,
	albumcount = gyear.Count(),
	albumandartist = from y in gyear
						select new
						{
							title = y.Title,
							artistname = y.Artist.Name,
							trackcount = (from p in y.Tracks 
											select p).Count()
						}
}

//grouping can be done on entity attributes determine using a 
//	navigational property. List tracks for Albums produced after 2010 by Genre Name.

from t in Tracks
where t.Album.ReleaseYear > 2010
group t by t.Genre.Name into gTemp
select new
{
	genre =  gTemp.Key,
	mumberof = gTemp.Count()
}

// Same Report but use the Entity as the Group.
// NOTE : When you have multiple attributes in your key
// 			you can refer to each key attribute separately

from t in Tracks
where t.Album.ReleaseYear > 2010
group t by t.Genre into gTemp
orderby gTemp.Key.Name
select new
{
	genre = gTemp.Key.Name,
	numberof = gTemp.Count()
}


//create a list of customers by employee support individual
//showing the employee lastname, firstname (phone) , the number
// of customers for this employee, and a customer list for the 
// employee by state, city, and customer firstname + last name

from c in Customers
group c by c.SupportRepIdEmployee into gTemp
select new
{
	employee = gTemp.Key.LastName + ", " + gTemp.Key.FirstName + " ( "+ gTemp.Key.Phone + ")",
	customerCount = gTemp.Count(),
	customers = from x in gTemp
				orderby x.State, x.City, x.LastName
				select new
				{
					state = x.State,
					city = x.City,
					name = x.LastName + ", " + x.FirstName
				}
	
}
