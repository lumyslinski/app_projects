![Scrapinghub logo](scrapinghub.png)

# Crawling spider in Scrapy #

The goal of this project is to scrape artistic work information from a [museum](http://pstrial-2017-12-18.toscrape.com).

It scrapes all works in the In Sunsh and Summertime categories. It navigates down the work browse tree (e.g.: Summertime / Wrapper From / Barn Owl) to the lowest level with all details including images.

Technologies: 
- Python with Scrapy 1.5.1
- Linux Ubuntu 18.04

# Output

```
/usr/bin/python3 /home/luk/.local/bin/scrapy crawl art
2018-09-15 23:35:49 [scrapy.utils.log] INFO: Scrapy 1.5.1 started (bot: artproject)
2018-09-15 23:35:49 [scrapy.utils.log] INFO: Versions: lxml 4.2.4.0, libxml2 2.9.8, cssselect 1.0.3, parsel 1.5.0, w3lib 1.19.0, Twisted 18.7.0, Python 3.6.5 (default, Apr  1 2018, 05:46:30) - [GCC 7.3.0], pyOpenSSL 18.0.0 (OpenSSL 1.1.0h  27 Mar 2018), cryptography 2.3, Platform Linux-4.15.0-33-generic-x86_64-with-Ubuntu-18.04-bionic
2018-09-15 23:35:49 [scrapy.crawler] INFO: Overridden settings: {'BOT_NAME': 'artproject', 'NEWSPIDER_MODULE': 'artproject.spiders', 'ROBOTSTXT_OBEY': True, 'SPIDER_MODULES': ['artproject.spiders']}
2018-09-15 23:35:49 [scrapy.middleware] INFO: Enabled extensions:
['scrapy.extensions.corestats.CoreStats',
 'scrapy.extensions.telnet.TelnetConsole',
 'scrapy.extensions.memusage.MemoryUsage',
 'scrapy.extensions.logstats.LogStats']
2018-09-15 23:35:49 [scrapy.middleware] INFO: Enabled downloader middlewares:
['scrapy.downloadermiddlewares.robotstxt.RobotsTxtMiddleware',
 'scrapy.downloadermiddlewares.httpauth.HttpAuthMiddleware',
 'scrapy.downloadermiddlewares.downloadtimeout.DownloadTimeoutMiddleware',
 'scrapy.downloadermiddlewares.defaultheaders.DefaultHeadersMiddleware',
 'scrapy.downloadermiddlewares.useragent.UserAgentMiddleware',
 'scrapy.downloadermiddlewares.retry.RetryMiddleware',
 'scrapy.downloadermiddlewares.redirect.MetaRefreshMiddleware',
 'scrapy.downloadermiddlewares.httpcompression.HttpCompressionMiddleware',
 'scrapy.downloadermiddlewares.redirect.RedirectMiddleware',
 'scrapy.downloadermiddlewares.cookies.CookiesMiddleware',
 'scrapy.downloadermiddlewares.httpproxy.HttpProxyMiddleware',
 'scrapy.downloadermiddlewares.stats.DownloaderStats']
2018-09-15 23:35:49 [scrapy.middleware] INFO: Enabled spider middlewares:
['scrapy.spidermiddlewares.httperror.HttpErrorMiddleware',
 'scrapy.spidermiddlewares.offsite.OffsiteMiddleware',
 'scrapy.spidermiddlewares.referer.RefererMiddleware',
 'scrapy.spidermiddlewares.urllength.UrlLengthMiddleware',
 'scrapy.spidermiddlewares.depth.DepthMiddleware']
2018-09-15 23:35:49 [scrapy.middleware] INFO: Enabled item pipelines:
[]
2018-09-15 23:35:49 [scrapy.core.engine] INFO: Spider opened
2018-09-15 23:35:49 [scrapy.extensions.logstats] INFO: Crawled 0 pages (at 0 pages/min), scraped 0 items (at 0 items/min)
2018-09-15 23:35:49 [scrapy.extensions.telnet] DEBUG: Telnet console listening on 127.0.0.1:6023
2018-09-15 23:35:49 [scrapy.core.engine] DEBUG: Crawled (404) <GET http://pstrial-2017-12-18.toscrape.com/robots.txt> (referer: None)
2018-09-15 23:35:51 [scrapy.core.engine] DEBUG: Crawled (200) <GET http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/barnowl?page=1> (referer: http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/barnowl/)
2018-09-15 23:35:51 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/barnowl?page=1>
{'artist': ["[' D. Y. Cameron']"],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_12686.jpg',
 'path': ['summertime', 'wrapperfrom', 'barnowl'],
 'title': 'Muckrach Castle, Scotland',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/12686/Muckrach_Castle_Scotland?back=177',
 'width': 0}
2018-09-15 23:35:51 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/barnowl?page=1>
{'artist': ["[' Frederick B. Scheel']"],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_12776.jpg',
 'path': ['summertime', 'wrapperfrom', 'barnowl'],
 'title': 'M. Favazza of Gloucester',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/12776/M_Favazza_of_Gloucester?back=177',
 'width': 0}
2018-09-15 23:35:51 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/barnowl?page=1>
{'artist': [],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_12858.jpg',
 'path': ['summertime', 'wrapperfrom', 'barnowl'],
 'title': 'Tunic',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/12858/Tunic?back=177',
 'width': 0}
2018-09-15 23:35:51 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/barnowl?page=1>
{'artist': ["[' Christian Burchard']"],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_13009.jpg',
 'path': ['summertime', 'wrapperfrom', 'barnowl'],
 'title': 'Basket, from a set of 22',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/13009/Basket_from_a_set_of_22?back=177',
 'width': 0}
2018-09-15 23:35:51 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/barnowl?page=1>
{'artist': ["[' Frank Gaard",
            '  Stuart Mead',
            ' Author: Frank Gaard',
            ' Author: Stuart Mead',
            "  Le Dernier CRI, Marseille, France']"],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_13144.jpg',
 'path': ['summertime', 'wrapperfrom', 'barnowl'],
 'title': 'The Immortal Man Bag Journal of Art',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/13144/The_Immortal_Man_Bag_Journal_o?back=177',
 'width': 0}

...

2018-09-15 23:51:04 [scrapy.core.engine] INFO: Closing spider (finished)
2018-09-15 23:51:04 [scrapy.statscollectors] INFO: Dumping Scrapy stats:
{'downloader/request_bytes': 1289274,
 'downloader/request_count': 3635,
 'downloader/request_method_count/GET': 3635,
 'downloader/response_bytes': 5233639,
 'downloader/response_count': 3635,
 'downloader/response_status_count/200': 3634,
 'downloader/response_status_count/404': 1,
 'finish_reason': 'finished',
 'finish_time': datetime.datetime(2018, 9, 15, 21, 51, 4, 271644),
 'item_scraped_count': 6600,
 'log_count/DEBUG': 10236,
 'log_count/INFO': 22,
 'memusage/max': 1032257536,
 'memusage/startup': 1032257536,
 'request_depth_max': 113,
 'response_received_count': 3635,
 'scheduler/dequeued': 3634,
 'scheduler/dequeued/memory': 3634,
 'scheduler/enqueued': 3634,
 'scheduler/enqueued/memory': 3634,
 'start_time': datetime.datetime(2018, 9, 15, 21, 35, 49, 151179)}
2018-09-15 23:51:04 [scrapy.core.engine] INFO: Spider closed (finished)
```