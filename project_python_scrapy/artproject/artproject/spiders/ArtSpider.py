import scrapy
from artproject.items import ArtprojectItem
from scrapy.spiders import Rule
from scrapy.linkextractors import LinkExtractor


class ArtSpider(scrapy.Spider):
    emptyItemsTryCount = 0
    name = "art"
    baseUrl = "http://pstrial-2017-12-18.toscrape.com"
    categories = ['summertime', 'wrapperfrom', 'barnowl']
    Rules = (
    Rule(LinkExtractor(allow=(), restrict_xpaths=('//a[@text="Next"]',)), callback="parse", follow=True),)

    def start_requests(self):
        combinedCategory = ''
        for category in self.categories:
            combinedCategory += category + '/'
            destinationUrl = self.baseUrl + "/browse/" + combinedCategory
            yield scrapy.Request(url=destinationUrl, callback=self.parse)

    def parse(self, response):
    # It should navigate down the work browse tree (e.g.: Summertime / Wrapper From / Barn Owl)
    # to the lowest level and parse on a per-work basis.
    # If a work does not have information for a specific field, please omit the field.
        stringUrl = str(response.request.url)
        categoriesList = []
        for category in self.categories:
            if category in stringUrl:
                categoriesList.append(category)

        a_selectors = response.xpath('//a[contains(@href,"item")]')
        if len(a_selectors) > 0:
            for a in a_selectors:
                item = ArtprojectItem()
                item['url'] = self.baseUrl + a.xpath("@href").extract_first()
                item['path'] = categoriesList
                item['artist'] = []
                artists = str(a.xpath("./div/h2/text()").extract())
                artists = self.cleanArtist(artists).split(";")
                for artist in artists:
                    if artist != "[]" and artist is not None:
                        item['artist'].append(artist.strip('\\\n\t\r'))
                item['title'] = a.xpath("./div/h1/text()").extract_first()
                item['image'] = self.baseUrl + a.xpath("./img/@src").extract_first()
                h = a.xpath("./img/@height").extract_first()
                if h == None:
                    item['height'] = 0
                else:
                    item['height'] = h
                w = a.xpath("./img/@width").extract_first()
                if w == None:
                    item['width'] = 0
                else:
                    item['width'] = h
                # description is propably in specific work item
                if len(item['url']) > 0:
                    yield scrapy.Request(item['url'], callback=self.parse_specific_work_item_tags, meta={'item': item})
                yield item
        else:
            #count how many times we do not have items on the page, if there is more than 3 then we will stop
            self.emptyItemsTryCount += 1

        if self.emptyItemsTryCount < 3:
            # follow next page links
            next_page = response.xpath('//a[contains(text(),"Next")]/@href').extract()
            if next_page and len(a_selectors) > 0:
                next_href = next_page[0]
                next_page_url = self.baseUrl + next_href
                request = scrapy.Request(url=next_page_url, callback=self.parse)
                yield request


    def parse_specific_work_item_tags(self, response):
        item = response.meta['item']
        item['description'] = response.xpath('//div[contains(@itemprop,"description")]/p/text()').extract()
        yield item

    def cleanArtist(self, artists):
        if "Publisher:" in artists:
            artists = artists.replace("Publisher:", "")
        if "Photographer:" in artists:
            artists = artists.replace("Photographer:", "")
        if "Artist:" in artists:
            artists = artists.replace("Artist:", "")
        if "Maker:" in artists:
            artists = artists.replace("Maker:", "")
        if "Designer:" in artists:
            artists = artists.replace("Designer:", "")
        return artists