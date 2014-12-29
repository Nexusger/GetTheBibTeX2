var topSearchResults = new Bloodhound({
    datumTokenizer: Bloodhound.tokenizers.obj.whitespace("Key"),
    queryTokenizer: Bloodhound.tokenizers.whitespace,
    limit: 10,
    prefetch: '/api/Prefetch/GetConferences',
   // remote: '/api/Query/GetResults/query=%QUERY'
});
topSearchResults.initialize();
//var topPersonSearchResults = new Bloodhound({
//    datumTokenizer: Bloodhound.tokenizers.obj.whitespace("DisplayText"),
//    queryTokenizer: Bloodhound.tokenizers.whitespace,
//    limit: 10,
//    prefetch: '/api/Prefetch/GetPeople',
//    remote: '/api/Query/GetResults/query=%QUERY'

//});
//topPersonSearchResults.initialize();

//var searches = [{ "Key": "homepages/d/TorbenDohrn", "FoundBy": "none", "DisplayText": "Torben Dohrn", "SearchResultSourceType": 0 }, { "Key": "homepages/d/FabianDohrn", "FoundBy": "none", "DisplayText": "Fred Dohrn", "SearchResultSourceType": 0 }, { "Key": "conf/l/lak", "FoundBy": "none", "DisplayText": "Learning Analyics Konference", "SearchResultSourceType": 1 }];
//var lok = ['Fred', 'Bubl'];
//var states = new Bloodhound({
//    datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
//    queryTokenizer: Bloodhound.tokenizers.whitespace,
//    // `states` is an array of state names defined in "The Basics"
//    local: $.map(searches, function (singleSearch) { return { value: singleSearch.DisplayText }; })
//});
//states.initialize();
//var keys = new Bloodhound({
//    datumTokenizer: Bloodhound.tokenizers.obj.whitespace('klo'),
//    queryTokenizer: Bloodhound.tokenizers.whitespace,
//    // `states` is an array of state names defined in "The Basics"
//    local: $.map(lok, function (lus) { return { klo: lus }; })
//});

//keys.initialize();

$(document).ready(function () {
    $('#search .typeahead').typeahead(
    {
        hint: true,
        highlight: true,
        minLength: 1
    },
    {
        name: 'topSearchResults',
        displayKey: 'Key',
        // `ttAdapter` wraps the suggestion engine in an adapter that
        // is compatible with the typeahead jQuery plugin
        source: topSearchResults.ttAdapter(),
        templates: {
            header: '<h3>Publikationen</h3>',
            empty: [
                '<div class="empty-message">',
                'Es konnten keine Publikationen gefunden werden',
                '</div>'
            ].join('\n'),
}
    }

    //,{
    //    name: 'topPersonSearchResults',
    //    displayKey: 'DisplayText',
    //    // `ttAdapter` wraps the suggestion engine in an adapter that
    //    // is compatible with the typeahead jQuery plugin
    //    source: topPersonSearchResults.ttAdapter(),
    //    templates: {
    //        header: '<h3 class="league-name">Personen</h3>'
    //    }
    //}
    ).on('typeahead:selected', function (obj, datum) {
        //sendAjaxRequest("GET", function() {}, datum.Key);
        getAllItems(datum.Key);
    });
});
