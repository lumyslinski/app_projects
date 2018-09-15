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
2018-09-15 22:57:34 [scrapy.utils.log] INFO: Scrapy 1.5.1 started (bot: artproject)
2018-09-15 22:57:34 [scrapy.utils.log] INFO: Versions: lxml 4.2.4.0, libxml2 2.9.8, cssselect 1.0.3, parsel 1.5.0, w3lib 1.19.0, Twisted 18.7.0, Python 3.6.5 (default, Apr  1 2018, 05:46:30) - [GCC 7.3.0], pyOpenSSL 18.0.0 (OpenSSL 1.1.0h  27 Mar 2018), cryptography 2.3, Platform Linux-4.15.0-33-generic-x86_64-with-Ubuntu-18.04-bionic
2018-09-15 22:57:34 [scrapy.crawler] INFO: Overridden settings: {'BOT_NAME': 'artproject', 'NEWSPIDER_MODULE': 'artproject.spiders', 'ROBOTSTXT_OBEY': True, 'SPIDER_MODULES': ['artproject.spiders']}
2018-09-15 22:57:34 [scrapy.middleware] INFO: Enabled extensions:
['scrapy.extensions.corestats.CoreStats',
 'scrapy.extensions.telnet.TelnetConsole',
 'scrapy.extensions.memusage.MemoryUsage',
 'scrapy.extensions.logstats.LogStats']
2018-09-15 22:57:34 [scrapy.middleware] INFO: Enabled downloader middlewares:
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
2018-09-15 22:57:34 [scrapy.middleware] INFO: Enabled spider middlewares:
['scrapy.spidermiddlewares.httperror.HttpErrorMiddleware',
 'scrapy.spidermiddlewares.offsite.OffsiteMiddleware',
 'scrapy.spidermiddlewares.referer.RefererMiddleware',
 'scrapy.spidermiddlewares.urllength.UrlLengthMiddleware',
 'scrapy.spidermiddlewares.depth.DepthMiddleware']
2018-09-15 22:57:34 [scrapy.middleware] INFO: Enabled item pipelines:
[]
2018-09-15 22:57:34 [scrapy.core.engine] INFO: Spider opened
2018-09-15 22:57:34 [scrapy.extensions.logstats] INFO: Crawled 0 pages (at 0 pages/min), scraped 0 items (at 0 items/min)
2018-09-15 22:57:34 [scrapy.extensions.telnet] DEBUG: Telnet console listening on 127.0.0.1:6023
2018-09-15 22:57:35 [scrapy.core.engine] DEBUG: Crawled (404) <GET http://pstrial-2017-12-18.toscrape.com/robots.txt> (referer: None)
2018-09-15 22:57:35 [scrapy.core.engine] DEBUG: Crawled (200) <GET http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/> (referer: None)
2018-09-15 22:57:35 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/>
{'artist': ["[' Louis-Georges Brillouin']"],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11376.jpg',
 'path': ['summertime', 'wrapperfrom'],
 'title': 'View of the Tabularium, Rome',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11376/View_of_the_Tabularium_Rome?back=169',
 'width': 0}
2018-09-15 22:57:35 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/>
{'artist': ["[' Antonio Senape']"],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11399.jpg',
 'path': ['summertime', 'wrapperfrom'],
 'title': 'View of Siracusa',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11399/View_of_Siracusa?back=169',
 'width': 0}
2018-09-15 22:57:35 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/>
{'artist': ["[' Christopher Faust']"],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11564.jpg',
 'path': ['summertime', 'wrapperfrom'],
 'title': 'Detail Canon Beach, Oregon',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11564/Detail_Canon_Beach_Oregon?back=169',
 'width': 0}
2018-09-15 22:57:35 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/>
{'artist': [],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11757.jpg',
 'path': ['summertime', 'wrapperfrom'],
 'title': 'Huaxi Stitching Design',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11757/Huaxi_Stitching_Design?back=169',
 'width': 0}
2018-09-15 22:57:35 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/>
{'artist': ["[' Gilles Peress']"],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11779.jpg',
 'path': ['summertime', 'wrapperfrom'],
 'title': 'Rwanda',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11779/Rwanda?back=169',
 'width': 0}
2018-09-15 22:57:35 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/>
{'artist': ["[' Unknown Amish']"],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11823.jpg',
 'path': ['summertime', 'wrapperfrom'],
 'title': 'Amish Crib Quilt',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11823/Amish_Crib_Quilt?back=169',
 'width': 0}
2018-09-15 22:57:35 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/>
{'artist': [],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11856.jpg',
 'path': ['summertime', 'wrapperfrom'],
 'title': 'Sedan Document Box',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11856/Sedan_Document_Box?back=169',
 'width': 0}
2018-09-15 22:57:35 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/>
{'artist': ["[' Wallace Nutting']"],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11933.jpg',
 'path': ['summertime', 'wrapperfrom'],
 'title': 'Untitled [river and trees]',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11933/Untitled_river_and_trees_?back=169',
 'width': 0}
2018-09-15 22:57:35 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/>
{'artist': [],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11949.jpg',
 'path': ['summertime', 'wrapperfrom'],
 'title': 'Sash',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11949/Sash?back=169',
 'width': 0}
2018-09-15 22:57:35 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/>
{'artist': [],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_12039.jpg',
 'path': ['summertime', 'wrapperfrom'],
 'title': 'Auspicious Pillow Decorations',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/12039/Auspicious_Pillow_Decorations?back=169',
 'width': 0}
2018-09-15 22:57:35 [scrapy.core.scraper] ERROR: Spider error processing <GET http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/> (referer: None)
Traceback (most recent call last):
  File "/home/luk/.local/lib/python3.6/site-packages/scrapy/utils/defer.py", line 102, in iter_errback
    yield next(it)
  File "/home/luk/.local/lib/python3.6/site-packages/scrapy/spidermiddlewares/offsite.py", line 30, in process_spider_output
    for x in result:
  File "/home/luk/.local/lib/python3.6/site-packages/scrapy/spidermiddlewares/referer.py", line 339, in <genexpr>
    return (_set_referer(r) for r in result or ())
  File "/home/luk/.local/lib/python3.6/site-packages/scrapy/spidermiddlewares/urllength.py", line 37, in <genexpr>
    return (r for r in result or () if _filter(r))
  File "/home/luk/.local/lib/python3.6/site-packages/scrapy/spidermiddlewares/depth.py", line 58, in <genexpr>
    return (r for r in result or () if _filter(r))
  File "/mnt/data/Work/lumyslinski/app_projects/project_python_scrapy/artproject/artproject/spiders/ArtSpider.py", line 60, in parse
    if next_page & len(a_selectors) > 0:
TypeError: unsupported operand type(s) for &: 'list' and 'int'
2018-09-15 22:57:36 [scrapy.core.engine] DEBUG: Crawled (200) <GET http://pstrial-2017-12-18.toscrape.com/item/11564/Detail_Canon_Beach_Oregon?back=169> (referer: http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/)
2018-09-15 22:57:36 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/item/11564/Detail_Canon_Beach_Oregon?back=169>
{'artist': ["[' Christopher Faust']"],
 'description': ['water and rocks; horizon line at middle of image; light grey '
                 'sky'],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11564.jpg',
 'path': ['summertime', 'wrapperfrom'],
 'title': 'Detail Canon Beach, Oregon',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11564/Detail_Canon_Beach_Oregon?back=169',
 'width': 0}
2018-09-15 22:57:37 [scrapy.core.engine] DEBUG: Crawled (200) <GET http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/barnowl/> (referer: None)
2018-09-15 22:57:37 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/barnowl/>
{'artist': ["[' Henri Lehmann']"],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11291.jpg',
 'path': ['summertime', 'wrapperfrom', 'barnowl'],
 'title': 'Draped Lamenting Figure',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11291/Draped_Lamenting_Figure?back=177',
 'width': 0}
2018-09-15 22:57:37 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/barnowl/>
{'artist': ["[' Antonio Acquaroni']"],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11454.jpg',
 'path': ['summertime', 'wrapperfrom', 'barnowl'],
 'title': 'Forum of Trajan, Rome',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11454/Forum_of_Trajan_Rome?back=177',
 'width': 0}
2018-09-15 22:57:37 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/barnowl/>
{'artist': ["[' Erickson and Weiss']"],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11676.jpg',
 'path': ['summertime', 'wrapperfrom', 'barnowl'],
 'title': 'Furnishing Fabric',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11676/Furnishing_Fabric?back=177',
 'width': 0}
2018-09-15 22:57:37 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/barnowl/>
{'artist': ["[' Possibly J.W. Meeks']"],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11726.jpg',
 'path': ['summertime', 'wrapperfrom', 'barnowl'],
 'title': 'Gothic Revival chair',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11726/Gothic_Revival_chair?back=177',
 'width': 0}
2018-09-15 22:57:37 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/barnowl/>
{'artist': ["[' Gilles Peress']"],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11819.jpg',
 'path': ['summertime', 'wrapperfrom', 'barnowl'],
 'title': 'Rwanda',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11819/Rwanda?back=177',
 'width': 0}
2018-09-15 22:57:37 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/barnowl/>
{'artist': ["[' Lynn Geesaman']"],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11924.jpg',
 'path': ['summertime', 'wrapperfrom', 'barnowl'],
 'title': 'Trees, Dartmoor, England',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11924/Trees_Dartmoor_England?back=177',
 'width': 0}
2018-09-15 22:57:37 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/barnowl/>
{'artist': [],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11952.jpg',
 'path': ['summertime', 'wrapperfrom', 'barnowl'],
 'title': 'Batik Sample',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11952/Batik_Sample?back=177',
 'width': 0}
2018-09-15 22:57:37 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/barnowl/>
{'artist': [],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11996.jpg',
 'path': ['summertime', 'wrapperfrom', 'barnowl'],
 'title': 'Crib quilt',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11996/Crib_quilt?back=177',
 'width': 0}
2018-09-15 22:57:37 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/barnowl/>
{'artist': ["[' Richard Uhlemeyer']"],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_12265.jpg',
 'path': ['summertime', 'wrapperfrom', 'barnowl'],
 'title': 'Vase',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/12265/Vase?back=177',
 'width': 0}
2018-09-15 22:57:37 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/barnowl/>
{'artist': [],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_12291.jpg',
 'path': ['summertime', 'wrapperfrom', 'barnowl'],
 'title': 'Skirt',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/12291/Skirt?back=177',
 'width': 0}
2018-09-15 22:57:37 [scrapy.core.scraper] ERROR: Spider error processing <GET http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/barnowl/> (referer: None)
Traceback (most recent call last):
  File "/home/luk/.local/lib/python3.6/site-packages/scrapy/utils/defer.py", line 102, in iter_errback
    yield next(it)
  File "/home/luk/.local/lib/python3.6/site-packages/scrapy/spidermiddlewares/offsite.py", line 30, in process_spider_output
    for x in result:
  File "/home/luk/.local/lib/python3.6/site-packages/scrapy/spidermiddlewares/referer.py", line 339, in <genexpr>
    return (_set_referer(r) for r in result or ())
  File "/home/luk/.local/lib/python3.6/site-packages/scrapy/spidermiddlewares/urllength.py", line 37, in <genexpr>
    return (r for r in result or () if _filter(r))
  File "/home/luk/.local/lib/python3.6/site-packages/scrapy/spidermiddlewares/depth.py", line 58, in <genexpr>
    return (r for r in result or () if _filter(r))
  File "/mnt/data/Work/lumyslinski/app_projects/project_python_scrapy/artproject/artproject/spiders/ArtSpider.py", line 60, in parse
    if next_page & len(a_selectors) > 0:
TypeError: unsupported operand type(s) for &: 'list' and 'int'
2018-09-15 22:57:37 [scrapy.core.engine] DEBUG: Crawled (200) <GET http://pstrial-2017-12-18.toscrape.com/item/11376/View_of_the_Tabularium_Rome?back=169> (referer: http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/)
2018-09-15 22:57:37 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/item/11376/View_of_the_Tabularium_Rome?back=169>
{'artist': ["[' Louis-Georges Brillouin']"],
 'description': ['view through arches with square columns at right'],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11376.jpg',
 'path': ['summertime', 'wrapperfrom'],
 'title': 'View of the Tabularium, Rome',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11376/View_of_the_Tabularium_Rome?back=169',
 'width': 0}
2018-09-15 22:57:37 [scrapy.core.engine] DEBUG: Crawled (200) <GET http://pstrial-2017-12-18.toscrape.com/browse/summertime/> (referer: None)
2018-09-15 22:57:37 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/browse/summertime/>
{'artist': [],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11378.jpg',
 'path': ['summertime'],
 'title': 'Dance skirt',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11378/Dance_skirt?back=168',
 'width': 0}
2018-09-15 22:57:37 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/browse/summertime/>
{'artist': ["[' Kenneth D. Snelson']"],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11430.jpg',
 'path': ['summertime'],
 'title': 'Tokyo, Hakusansonso, Garden with Pond and Stone Bridge',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11430/Tokyo_Hakusansonso_Garden_with?back=168',
 'width': 0}
2018-09-15 22:57:37 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/browse/summertime/>
{'artist': [],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11493.jpg',
 'path': ['summertime'],
 'title': 'Abia game token',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11493/Abia_game_token?back=168',
 'width': 0}
2018-09-15 22:57:37 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/browse/summertime/>
{'artist': [],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11515.jpg',
 'path': ['summertime'],
 'title': 'Untitled [two red and green birds]',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11515/Untitled_two_red_and_green_bir?back=168',
 'width': 0}
2018-09-15 22:57:37 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/browse/summertime/>
{'artist': ["[' Les Blacklock']"],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11530.jpg',
 'path': ['summertime'],
 'title': 'Untitled [red branch in snow]',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11530/Untitled_red_branch_in_snow_?back=168',
 'width': 0}
2018-09-15 22:57:37 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/browse/summertime/>
{'artist': ["[' Huntington Wilherill']"],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11559.jpg',
 'path': ['summertime'],
 'title': 'Sand Dunes, Death Valley',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11559/Sand_Dunes_Death_Valley?back=168',
 'width': 0}
2018-09-15 22:57:37 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/browse/summertime/>
{'artist': [],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11790.jpg',
 'path': ['summertime'],
 'title': 'Pants',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11790/Pants?back=168',
 'width': 0}
2018-09-15 22:57:37 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/browse/summertime/>
{'artist': [],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11794.jpg',
 'path': ['summertime'],
 'title': 'Hip and Shoulder Wrap Cloths (Hinggi)',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11794/Hip_and_Shoulder_Wrap_Cloths_H?back=168',
 'width': 0}
2018-09-15 22:57:37 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/browse/summertime/>
{'artist': ["[' Gilles Peress']"],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11881.jpg',
 'path': ['summertime'],
 'title': 'Rwanda',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11881/Rwanda?back=168',
 'width': 0}
2018-09-15 22:57:37 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/browse/summertime/>
{'artist': ["[' Lida Moser']"],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11999.jpg',
 'path': ['summertime'],
 'title': 'John Koch in His Studio, End of Day',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11999/John_Koch_in_His_Studio_End_of?back=168',
 'width': 0}
2018-09-15 22:57:37 [scrapy.core.scraper] ERROR: Spider error processing <GET http://pstrial-2017-12-18.toscrape.com/browse/summertime/> (referer: None)
Traceback (most recent call last):
  File "/home/luk/.local/lib/python3.6/site-packages/scrapy/utils/defer.py", line 102, in iter_errback
    yield next(it)
  File "/home/luk/.local/lib/python3.6/site-packages/scrapy/spidermiddlewares/offsite.py", line 30, in process_spider_output
    for x in result:
  File "/home/luk/.local/lib/python3.6/site-packages/scrapy/spidermiddlewares/referer.py", line 339, in <genexpr>
    return (_set_referer(r) for r in result or ())
  File "/home/luk/.local/lib/python3.6/site-packages/scrapy/spidermiddlewares/urllength.py", line 37, in <genexpr>
    return (r for r in result or () if _filter(r))
  File "/home/luk/.local/lib/python3.6/site-packages/scrapy/spidermiddlewares/depth.py", line 58, in <genexpr>
    return (r for r in result or () if _filter(r))
  File "/mnt/data/Work/lumyslinski/app_projects/project_python_scrapy/artproject/artproject/spiders/ArtSpider.py", line 60, in parse
    if next_page & len(a_selectors) > 0:
TypeError: unsupported operand type(s) for &: 'list' and 'int'
2018-09-15 22:57:37 [scrapy.core.engine] DEBUG: Crawled (200) <GET http://pstrial-2017-12-18.toscrape.com/item/11856/Sedan_Document_Box?back=169> (referer: http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/)
2018-09-15 22:57:37 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/item/11856/Sedan_Document_Box?back=169>
{'artist': [],
 'description': ['dark huang hua-li T-shaped box with silver inset hardware at '
                 'all corners and a large central circular latch on front; '
                 'inside there are two small compartments with flip lids on '
                 'either side of a removable rectangular tray at center'],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11856.jpg',
 'path': ['summertime', 'wrapperfrom'],
 'title': 'Sedan Document Box',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11856/Sedan_Document_Box?back=169',
 'width': 0}
2018-09-15 22:57:37 [scrapy.core.engine] DEBUG: Crawled (200) <GET http://pstrial-2017-12-18.toscrape.com/item/11399/View_of_Siracusa?back=169> (referer: http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/)
2018-09-15 22:57:37 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/item/11399/View_of_Siracusa?back=169>
{'artist': ["[' Antonio Senape']"],
 'description': ['ruins, an ancient theater with semicircular descending rows '
                 'of seating; water in distance with sailboats; island with '
                 'buildings in center backgroundt; foliage and buildings in '
                 'foreground, with four figures--three standing and one '
                 'seated, drawing'],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11399.jpg',
 'path': ['summertime', 'wrapperfrom'],
 'title': 'View of Siracusa',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11399/View_of_Siracusa?back=169',
 'width': 0}
2018-09-15 22:57:37 [scrapy.core.engine] DEBUG: Crawled (200) <GET http://pstrial-2017-12-18.toscrape.com/item/11779/Rwanda?back=169> (referer: http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/)
2018-09-15 22:57:38 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/item/11779/Rwanda?back=169>
{'artist': ["[' Gilles Peress']"],
 'description': ['boy scooping food off the ground with his hands'],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11779.jpg',
 'path': ['summertime', 'wrapperfrom'],
 'title': 'Rwanda',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11779/Rwanda?back=169',
 'width': 0}
2018-09-15 22:57:38 [scrapy.core.engine] DEBUG: Crawled (200) <GET http://pstrial-2017-12-18.toscrape.com/item/12039/Auspicious_Pillow_Decorations?back=169> (referer: http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/)
2018-09-15 22:57:38 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/item/12039/Auspicious_Pillow_Decorations?back=169>
{'artist': [],
 'description': ['embroidered auspicious characters'],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_12039.jpg',
 'path': ['summertime', 'wrapperfrom'],
 'title': 'Auspicious Pillow Decorations',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/12039/Auspicious_Pillow_Decorations?back=169',
 'width': 0}
2018-09-15 22:57:38 [scrapy.core.engine] DEBUG: Crawled (200) <GET http://pstrial-2017-12-18.toscrape.com/item/11291/Draped_Lamenting_Figure?back=177> (referer: http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/barnowl/)
2018-09-15 22:57:38 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/item/11291/Draped_Lamenting_Figure?back=177>
{'artist': ["[' Henri Lehmann']"],
 'description': ['seated figure completely enveloped in drapery, with head '
                 'bent down and PL knee raised'],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11291.jpg',
 'path': ['summertime', 'wrapperfrom', 'barnowl'],
 'title': 'Draped Lamenting Figure',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11291/Draped_Lamenting_Figure?back=177',
 'width': 0}
2018-09-15 22:57:38 [scrapy.core.engine] DEBUG: Crawled (200) <GET http://pstrial-2017-12-18.toscrape.com/item/11933/Untitled_river_and_trees_?back=169> (referer: http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/)
2018-09-15 22:57:38 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/item/11933/Untitled_river_and_trees_?back=169>
{'artist': ["[' Wallace Nutting']"],
 'description': [],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11933.jpg',
 'path': ['summertime', 'wrapperfrom'],
 'title': 'Untitled [river and trees]',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11933/Untitled_river_and_trees_?back=169',
 'width': 0}
2018-09-15 22:57:38 [scrapy.core.engine] DEBUG: Crawled (200) <GET http://pstrial-2017-12-18.toscrape.com/item/11949/Sash?back=169> (referer: http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/)
2018-09-15 22:57:38 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/item/11949/Sash?back=169>
{'artist': [],
 'description': ['woven geometric pattern in red, purple, magenta, turquoise, '
                 'yellow, black and white'],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11949.jpg',
 'path': ['summertime', 'wrapperfrom'],
 'title': 'Sash',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11949/Sash?back=169',
 'width': 0}
2018-09-15 22:57:39 [scrapy.core.engine] DEBUG: Crawled (200) <GET http://pstrial-2017-12-18.toscrape.com/item/11924/Trees_Dartmoor_England?back=177> (referer: http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/barnowl/)
2018-09-15 22:57:39 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/item/11924/Trees_Dartmoor_England?back=177>
{'artist': ["[' Lynn Geesaman']"],
 'description': [],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11924.jpg',
 'path': ['summertime', 'wrapperfrom', 'barnowl'],
 'title': 'Trees, Dartmoor, England',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11924/Trees_Dartmoor_England?back=177',
 'width': 0}
2018-09-15 22:57:39 [scrapy.core.engine] DEBUG: Crawled (200) <GET http://pstrial-2017-12-18.toscrape.com/item/11823/Amish_Crib_Quilt?back=169> (referer: http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/)
2018-09-15 22:57:39 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/item/11823/Amish_Crib_Quilt?back=169>
{'artist': ["[' Unknown Amish']"],
 'description': ['black and brown border surrounding single squares'],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11823.jpg',
 'path': ['summertime', 'wrapperfrom'],
 'title': 'Amish Crib Quilt',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11823/Amish_Crib_Quilt?back=169',
 'width': 0}
2018-09-15 22:57:40 [scrapy.core.engine] DEBUG: Crawled (200) <GET http://pstrial-2017-12-18.toscrape.com/item/11757/Huaxi_Stitching_Design?back=169> (referer: http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/)
2018-09-15 22:57:40 [scrapy.core.engine] DEBUG: Crawled (200) <GET http://pstrial-2017-12-18.toscrape.com/item/11454/Forum_of_Trajan_Rome?back=177> (referer: http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/barnowl/)
2018-09-15 22:57:40 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/item/11757/Huaxi_Stitching_Design?back=169>
{'artist': [],
 'description': ['white, pink, red, yellow and green discontinuous '
                 'supplementary weft patterning on black field; central cross '
                 'design inside square with three arrows on outside corners of '
                 'square pointing inward; white border on three sides'],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11757.jpg',
 'path': ['summertime', 'wrapperfrom'],
 'title': 'Huaxi Stitching Design',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11757/Huaxi_Stitching_Design?back=169',
 'width': 0}
2018-09-15 22:57:40 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/item/11454/Forum_of_Trajan_Rome?back=177>
{'artist': ["[' Antonio Acquaroni']"],
 'description': ['view from inside ruins of forum, with broken-off columns; '
                 'tall column with sculpture of standing figure at center; '
                 'Classical buildings in middle ground beyond ruins'],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11454.jpg',
 'path': ['summertime', 'wrapperfrom', 'barnowl'],
 'title': 'Forum of Trajan, Rome',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11454/Forum_of_Trajan_Rome?back=177',
 'width': 0}
2018-09-15 22:57:40 [scrapy.core.engine] DEBUG: Crawled (200) <GET http://pstrial-2017-12-18.toscrape.com/item/11726/Gothic_Revival_chair?back=177> (referer: http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/barnowl/)
2018-09-15 22:57:40 [scrapy.core.engine] DEBUG: Crawled (200) <GET http://pstrial-2017-12-18.toscrape.com/item/11819/Rwanda?back=177> (referer: http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/barnowl/)
2018-09-15 22:57:40 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/item/11726/Gothic_Revival_chair?back=177>
{'artist': ["[' Possibly J.W. Meeks']"],
 'description': ['Gothic Revival Chair'],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11726.jpg',
 'path': ['summertime', 'wrapperfrom', 'barnowl'],
 'title': 'Gothic Revival chair',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11726/Gothic_Revival_chair?back=177',
 'width': 0}
2018-09-15 22:57:40 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/item/11819/Rwanda?back=177>
{'artist': ["[' Gilles Peress']"],
 'description': ['man and woman sitting together, wrapped in a cloth'],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11819.jpg',
 'path': ['summertime', 'wrapperfrom', 'barnowl'],
 'title': 'Rwanda',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11819/Rwanda?back=177',
 'width': 0}
2018-09-15 22:57:40 [scrapy.core.engine] DEBUG: Crawled (200) <GET http://pstrial-2017-12-18.toscrape.com/item/11999/John_Koch_in_His_Studio_End_of?back=168> (referer: http://pstrial-2017-12-18.toscrape.com/browse/summertime/)
2018-09-15 22:57:40 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/item/11999/John_Koch_in_His_Studio_End_of?back=168>
{'artist': ["[' Lida Moser']"],
 'description': [],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11999.jpg',
 'path': ['summertime'],
 'title': 'John Koch in His Studio, End of Day',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11999/John_Koch_in_His_Studio_End_of?back=168',
 'width': 0}
2018-09-15 22:57:40 [scrapy.core.engine] DEBUG: Crawled (200) <GET http://pstrial-2017-12-18.toscrape.com/item/12291/Skirt?back=177> (referer: http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/barnowl/)
2018-09-15 22:57:40 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/item/12291/Skirt?back=177>
{'artist': [],
 'description': ['black wrap skirt with small pleating and red, white and blue '
                 'trim; two long black tie strings'],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_12291.jpg',
 'path': ['summertime', 'wrapperfrom', 'barnowl'],
 'title': 'Skirt',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/12291/Skirt?back=177',
 'width': 0}
2018-09-15 22:57:40 [scrapy.core.engine] DEBUG: Crawled (200) <GET http://pstrial-2017-12-18.toscrape.com/item/12265/Vase?back=177> (referer: http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/barnowl/)
2018-09-15 22:57:41 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/item/12265/Vase?back=177>
{'artist': ["[' Richard Uhlemeyer']"],
 'description': ['baluster-shaped'],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_12265.jpg',
 'path': ['summertime', 'wrapperfrom', 'barnowl'],
 'title': 'Vase',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/12265/Vase?back=177',
 'width': 0}
2018-09-15 22:57:41 [scrapy.core.engine] DEBUG: Crawled (200) <GET http://pstrial-2017-12-18.toscrape.com/item/11676/Furnishing_Fabric?back=177> (referer: http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/barnowl/)
2018-09-15 22:57:41 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/item/11676/Furnishing_Fabric?back=177>
{'artist': ["[' Erickson and Weiss']"],
 'description': ['Printed multicolor textile; flower boquets on beige '
                 'background'],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11676.jpg',
 'path': ['summertime', 'wrapperfrom', 'barnowl'],
 'title': 'Furnishing Fabric',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11676/Furnishing_Fabric?back=177',
 'width': 0}
2018-09-15 22:57:41 [scrapy.core.engine] DEBUG: Crawled (200) <GET http://pstrial-2017-12-18.toscrape.com/item/11881/Rwanda?back=168> (referer: http://pstrial-2017-12-18.toscrape.com/browse/summertime/)
2018-09-15 22:57:41 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/item/11881/Rwanda?back=168>
{'artist': ["[' Gilles Peress']"],
 'description': ['decomposing bodies scattered on the ground'],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11881.jpg',
 'path': ['summertime'],
 'title': 'Rwanda',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11881/Rwanda?back=168',
 'width': 0}
2018-09-15 22:57:41 [scrapy.core.engine] DEBUG: Crawled (200) <GET http://pstrial-2017-12-18.toscrape.com/item/11790/Pants?back=168> (referer: http://pstrial-2017-12-18.toscrape.com/browse/summertime/)
2018-09-15 22:57:41 [scrapy.core.engine] DEBUG: Crawled (200) <GET http://pstrial-2017-12-18.toscrape.com/item/11559/Sand_Dunes_Death_Valley?back=168> (referer: http://pstrial-2017-12-18.toscrape.com/browse/summertime/)
2018-09-15 22:57:41 [scrapy.core.engine] DEBUG: Crawled (200) <GET http://pstrial-2017-12-18.toscrape.com/item/11515/Untitled_two_red_and_green_bir?back=168> (referer: http://pstrial-2017-12-18.toscrape.com/browse/summertime/)
2018-09-15 22:57:41 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/item/11790/Pants?back=168>
{'artist': [],
 'description': ['bright blue cotton, beige cotton at waist, woven, '
                 'embroidered, charcoal plain weave bands at bottom of each '
                 'pant leg'],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11790.jpg',
 'path': ['summertime'],
 'title': 'Pants',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11790/Pants?back=168',
 'width': 0}
2018-09-15 22:57:41 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/item/11559/Sand_Dunes_Death_Valley?back=168>
{'artist': ["[' Huntington Wilherill']"],
 'description': ["sand dunes; foreground dune's left side smooth and very "
                 'light; right side rippled; second dune textured and '
                 'partially in shadow; dunes in distance textured and light; '
                 'ne smooth, light patch, egg shape in right background'],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11559.jpg',
 'path': ['summertime'],
 'title': 'Sand Dunes, Death Valley',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11559/Sand_Dunes_Death_Valley?back=168',
 'width': 0}
2018-09-15 22:57:42 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/item/11515/Untitled_two_red_and_green_bir?back=168>
{'artist': [],
 'description': ['two birds with rusty-red crest, face, neck, wing tips and '
                 'tail; back green; breast streaked green; yellow beaks; '
                 'seated on tree branch with pink buds and star shaped '
                 'flowers'],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11515.jpg',
 'path': ['summertime'],
 'title': 'Untitled [two red and green birds]',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11515/Untitled_two_red_and_green_bir?back=168',
 'width': 0}
2018-09-15 22:57:42 [scrapy.core.engine] DEBUG: Crawled (200) <GET http://pstrial-2017-12-18.toscrape.com/item/11530/Untitled_red_branch_in_snow_?back=168> (referer: http://pstrial-2017-12-18.toscrape.com/browse/summertime/)
2018-09-15 22:57:43 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/item/11530/Untitled_red_branch_in_snow_?back=168>
{'artist': ["[' Les Blacklock']"],
 'description': ['bare red branches extending up from snow; trunk of birch '
                 'tree at left; snow-covered pine branches in URQ'],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11530.jpg',
 'path': ['summertime'],
 'title': 'Untitled [red branch in snow]',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11530/Untitled_red_branch_in_snow_?back=168',
 'width': 0}
2018-09-15 22:57:43 [scrapy.core.engine] DEBUG: Crawled (200) <GET http://pstrial-2017-12-18.toscrape.com/item/11794/Hip_and_Shoulder_Wrap_Cloths_H?back=168> (referer: http://pstrial-2017-12-18.toscrape.com/browse/summertime/)
2018-09-15 22:57:43 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/item/11794/Hip_and_Shoulder_Wrap_Cloths_H?back=168>
{'artist': [],
 'description': ['each 2 panels sewn vertically; ikat patterning, '
                 'rust/red/blue/white, animal and geometric motifs'],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11794.jpg',
 'path': ['summertime'],
 'title': 'Hip and Shoulder Wrap Cloths (Hinggi)',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11794/Hip_and_Shoulder_Wrap_Cloths_H?back=168',
 'width': 0}
2018-09-15 22:57:43 [scrapy.core.engine] DEBUG: Crawled (200) <GET http://pstrial-2017-12-18.toscrape.com/item/11430/Tokyo_Hakusansonso_Garden_with?back=168> (referer: http://pstrial-2017-12-18.toscrape.com/browse/summertime/)
2018-09-15 22:57:43 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/item/11430/Tokyo_Hakusansonso_Garden_with?back=168>
{'artist': ["[' Kenneth D. Snelson']"],
 'description': ['panoramic view of a garden with trees, bushes and various '
                 'types of small plants--no blooming flowers; flat stone '
                 'plank-like bride at center bottom extending over pond toward '
                 'path of large stones; paved path at right; worn path at left '
                 'with exposed tree roots; several buildings with tile roofs '
                 'partially hidden in dense foliage'],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11430.jpg',
 'path': ['summertime'],
 'title': 'Tokyo, Hakusansonso, Garden with Pond and Stone Bridge',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11430/Tokyo_Hakusansonso_Garden_with?back=168',
 'width': 0}
2018-09-15 22:57:43 [scrapy.core.engine] DEBUG: Crawled (200) <GET http://pstrial-2017-12-18.toscrape.com/item/11378/Dance_skirt?back=168> (referer: http://pstrial-2017-12-18.toscrape.com/browse/summertime/)
2018-09-15 22:57:43 [scrapy.core.engine] DEBUG: Crawled (200) <GET http://pstrial-2017-12-18.toscrape.com/item/11493/Abia_game_token?back=168> (referer: http://pstrial-2017-12-18.toscrape.com/browse/summertime/)
2018-09-15 22:57:43 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/item/11378/Dance_skirt?back=168>
{'artist': [],
 'description': ['long tan textile with appliquéd tan and brown geometric '
                 'shapes; the geometric shapes include circles, squares, and '
                 'lines bent at a 90 degree angle; edges are hemmed'],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11378.jpg',
 'path': ['summertime'],
 'title': 'Dance skirt',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11378/Dance_skirt?back=168',
 'width': 0}
2018-09-15 22:57:43 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/item/11493/Abia_game_token?back=168>
{'artist': [],
 'description': ['football-shaped; carved on one side with antelope lying '
                 "down; two radiating circles on antelope's body"],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11493.jpg',
 'path': ['summertime'],
 'title': 'Abia game token',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11493/Abia_game_token?back=168',
 'width': 0}
2018-09-15 22:57:44 [scrapy.core.engine] DEBUG: Crawled (200) <GET http://pstrial-2017-12-18.toscrape.com/item/11996/Crib_quilt?back=177> (referer: http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/barnowl/)
2018-09-15 22:57:44 [scrapy.core.engine] DEBUG: Crawled (200) <GET http://pstrial-2017-12-18.toscrape.com/item/11952/Batik_Sample?back=177> (referer: http://pstrial-2017-12-18.toscrape.com/browse/summertime/wrapperfrom/barnowl/)
2018-09-15 22:57:44 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/item/11996/Crib_quilt?back=177>
{'artist': [],
 'description': ['30 squares enclosed within a black border done in ? design; '
                 'common elements include central black square and radiating '
                 'black triangles tacked down where corners of squares meet; '
                 'log cabin variation ?'],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11996.jpg',
 'path': ['summertime', 'wrapperfrom', 'barnowl'],
 'title': 'Crib quilt',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11996/Crib_quilt?back=177',
 'width': 0}
2018-09-15 22:57:44 [scrapy.core.scraper] DEBUG: Scraped from <200 http://pstrial-2017-12-18.toscrape.com/item/11952/Batik_Sample?back=177>
{'artist': [],
 'description': ['batik dark and light blue patched cloth with large diamond '
                 'shape in the middle'],
 'height': 0,
 'image': 'http://pstrial-2017-12-18.toscrape.com/content/thumb_11952.jpg',
 'path': ['summertime', 'wrapperfrom', 'barnowl'],
 'title': 'Batik Sample',
 'url': 'http://pstrial-2017-12-18.toscrape.com/item/11952/Batik_Sample?back=177',
 'width': 0}
2018-09-15 22:57:44 [scrapy.core.engine] INFO: Closing spider (finished)
2018-09-15 22:57:44 [scrapy.statscollectors] INFO: Dumping Scrapy stats:
{'downloader/request_bytes': 11411,
 'downloader/request_count': 34,
 'downloader/request_method_count/GET': 34,
 'downloader/response_bytes': 47742,
 'downloader/response_count': 34,
 'downloader/response_status_count/200': 33,
 'downloader/response_status_count/404': 1,
 'finish_reason': 'finished',
 'finish_time': datetime.datetime(2018, 9, 15, 20, 57, 44, 692573),
 'item_scraped_count': 60,
 'log_count/DEBUG': 95,
 'log_count/ERROR': 3,
 'log_count/INFO': 7,
 'memusage/max': 937512960,
 'memusage/startup': 937512960,
 'request_depth_max': 1,
 'response_received_count': 34,
 'scheduler/dequeued': 33,
 'scheduler/dequeued/memory': 33,
 'scheduler/enqueued': 33,
 'scheduler/enqueued/memory': 33,
 'spider_exceptions/TypeError': 3,
 'start_time': datetime.datetime(2018, 9, 15, 20, 57, 34, 853518)}
2018-09-15 22:57:44 [scrapy.core.engine] INFO: Spider closed (finished)

Process finished with exit code 0
```