The main purpose is to produce **RSS feed** to websites doesn't.

The application used in **combination** with a CRON job. The CRON job fires the spider to make the captures. Yes, you have to define it manual to your server.  

To have just one CRON job, the application using the `dacron.php`. Which inside calls the individual files exist to `assets` folder. Structured like this because each site has own logic.  

Afterwards the results appear when browse to `index.php`.  

When on a **RSS aggregator** add this link  
`https://domain.com/index.php?id=1&rss=1`

will query the dbase on table `feed_tems` where `site_id = 1` for the **past two days** and return the results as RSS.

when ecxlude the `rss` parameter displey a html page for this site.

When on a **RSS aggregator** add this link  
`https://domain.com/jobs/?rss=1&s=remote`

will query the dbase on table `feed_tems` where `title = 'remote'` for the **past two days** and return the results as RSS.

The table `feeds` is a dummy table used only to undestand if the a feed will be visible when browsing the index.php and also to display from where the feed is coming from. The user has to define there any file included in `assets` folder.  

Everytime you add a file to `assets` folder you have to update the `dacron.php` and create a new record to table `feeds`.  

You will see on the included sample parsers (assets folder), have something like   
`if ($minute > 20 && $minute < 40)
	return;`  

this is to restrict when will parse the site.  


# This project uses the following 3rd-party dependency :  
* [simplehtmldom](http://sourceforge.net/projects/simplehtmldom/)


## This project is no longer maintained  

Copyright (c) 2021 [PipisCrew](http://pipiscrew.com)

Licensed under the [MIT license](http://www.opensource.org/licenses/mit-license.php).
