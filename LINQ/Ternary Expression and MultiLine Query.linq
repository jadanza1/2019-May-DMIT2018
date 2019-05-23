<Query Kind="Statements">
  <Connection>
    <ID>c1a07453-b2f4-488e-9263-4e75afa78a5c</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//TERNARY OPERATOR
// Ternary Operator format : contiditions(s) ? true value : false value;

// create an alphabetic list of Albums by ReleaseLabel
// Albums Missing labels will be listed as "Unknown"

var results = from x in Albums 
				orderby x.ReleaseLabel
				select new
				{
					title = x.Title, 
					label = x.ReleaseLabel == null ? "Unknown" : x.ReleaseLabel
				};
results.Dump();				

// create an Alphabetic list of Albums by Decades
// 70s, 80s, 90s

var results2 = from x in Albums
				select new
				{
					title = x.Title,
					decade = x.ReleaseYear > 1969 && x.ReleaseYear < 1980 ? "70s" : 
							 x.ReleaseYear > 1979 && x.ReleaseYear < 1990 ? "80s" :
							 x.ReleaseYear > 1989 && x.ReleaseYear < 2000 ? "90s" :  "modern"
				};
				
results2.Dump();

//Using Multiple Steps Query to Obtain the required Data Query

	//Create a list whether a particular track length 
	// is greater than, less than or the average teack length
	
	//PROBLEM: I need the average track length before testing 
	// the individual track lenght against the average
	
	var result_average = (from x in Tracks 
							select x.Milliseconds).Average();
							
	result_average.Dump();
	
	var result3 = from x in Tracks
					select new
				{
					song = x.Name,
					length = x.Milliseconds > result_average ? "Long" :
							 x.Milliseconds < result_average ? "Short" : "Average" 
				};
				
	result3.Dump();