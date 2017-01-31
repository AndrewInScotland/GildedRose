# GildedRose


## How to build 
Open the solution in Visual Studio 2015, then Rebuild All. You can run the tests from whatever test tool you are using (I used ReSharper 2015).

## How to run
The WebServices project uses IISExpress, so it's easiest to run from Visual Studio. 

- From the solution properties, choose multiple startup projects:
	- GildedRose.IdentityServer
	- GildedRose.WebServices
- Start the solution.
- Run the GildedRose.ConsoleClient.exe from the command line (ConsoleClient\bin\Debug). 
- Run it multiple times to see what happens to the inventory as you buy items.

## Design decisions


- My current team has been using IdentityServer3 with good results, so I thought it would be an interesting exercise to apply it here. 
This implementation is all in-memory and represents the bare minimum implementation of IdentityServer3.
- I used a Service Layer to isolate business logic.
- I used a REST-type API, returning JSON (this is the easiest format for mobile and web teams to work with). 

## Choice of data format
As in Design Decisions above, I used Json for the data format, which will support the majority of downstream clients.
### Sample request:
To get the list of items, just hit this URL:

    http://localhost:14869/Inventory

###Sample response:

    "[{\"Id\":\"746E4CFB-7DF8-46BC-9A94-923F29D17907\",\"Name\":\"CSY 37\",
	\"Description\":\"37 foot cutter-rigged sloop\",\"Price\":40000,
	\"InventoryCount\":2},{\"Id\":\"2552E1C7-64C5-460F-B550-60F3A720033F\",
	\"Name\":\"Venture 24\",\"Description\":\"24 foot swing keel\",\"Price
	\":5000,\"InventoryCount\":10}]"

## How do we know if a user is authenticated?
The BuyItem method on the Inventory Controller sports the [Authorize] attribute, which requires that a valid JWT (Json Web Token) is supplied in the HTTP headers for that method. This behavior is configured in the Startup class.

## Is it always possible to buy an item?
Once the inventory count reaches zero, you can no longer buy an item. This logic is specified in the Service Layer.

## Notes
 
### ReSharper and StyleCop
I used my existing ReSharper and StyleCop settings, which use some non-default settings, such as allowing tabs instead of spaces and enforcing the use of "this".
StyleCop was implemented using the StyleCop.Analyzers NuGet package, and was enforced by setting the Warnings as Errors build setting.

### Integration Tests
I provided some unit tests, but didn't have to time to set up integration tests. 
Instead, the Console app demonstrates integration.

### Boats
Oh, I should at least mention the sample data. They are makes and models of sailing boats! 
 
## Comments
My solution is a very basic example of an inventory system. A more realistic solution must take into account a number of other factors, such as multiple stores and geographic regions, multiple colors or sizes, time of year, sales, and special promotions.

### If I had more time, I would:

- Add basic debug/info logging
- Add more exception handling
- Add a separation layer between the Service layer and the Data Access layer. As it stands currently, the 
controller can change an Item's property and it will be immediately reflected in the data layer, without even having to call SaveItem. This wouldn't happen in reality because we'd use some sort of persistence layer, which would create new instances of items as they are loaded, and then throw them away after being saved. But even then you usually want an isolation layer between the Controller and the Data Access Layer. I usually use DTO's (or Models) for this transformation, and use a dedicated Entity class in the Data Access Layer.
- Provide sample JavaScript code. This is useful for illustrating Ajax calls and Json formats to downstream teams.
