var conferences = new Bloodhound({
    datumTokenizer: Bloodhound.tokenizers.obj.whitespace("DisplayText"),
    queryTokenizer: Bloodhound.tokenizers.whitespace,
    limit: 10,
    prefetch: '/api/Prefetch/GetConferences',
    remote: '/api/Query/GetConferences/%QUERY'
});
conferences.initialize();

var publications = new Bloodhound({
    datumTokenizer: Bloodhound.tokenizers.obj.whitespace("DisplayText"),
    queryTokenizer: Bloodhound.tokenizers.whitespace,
    limit: 10,
    prefetch: '/api/Prefetch/GetConferences',
    remote: {
        url: '/api/Query/GetPublications/',
        replace: function (url, encodedQuery) {
            return url + encodeURIComponent($.trim(decodeURIComponent(encodedQuery)));
        }
    }
});
publications.initialize();

var authors = new Bloodhound({
    datumTokenizer: Bloodhound.tokenizers.obj.whitespace("DisplayText"),
    queryTokenizer: Bloodhound.tokenizers.whitespace,
    limit: 10,
    prefetch: '/api/Prefetch/GetAuthors',
    remote: {
        url: '/api/Query/GetAuthors/',
        replace: function (url, encodedQuery) {
            return url + encodeURIComponent($.trim(decodeURIComponent(encodedQuery)));
        }
    }
});
authors.initialize();

$(document).ready(function () {
    $('#search .typeahead').typeahead(
    {
        hint: true,
        highlight: true,
        minLength: 1
    },
    {
        name: 'conferences',
        displayKey: 'DisplayText',
        // `ttAdapter` wraps the suggestion engine in an adapter that
        // is compatible with the typeahead jQuery plugin
        source: conferences.ttAdapter(),
        templates: {
            header: '<h3>Konferenzen</h3>',
            empty: [
                '<div class="empty-message">',
                'Es konnten keine Konferenzen gefunden werden',
                '</div>'
            ].join('\n'),
        }
    }
    , {
        name: 'publications',
        displayKey: 'DisplayText',
        // `ttAdapter` wraps the suggestion engine in an adapter that
        // is compatible with the typeahead jQuery plugin
        source: publications.ttAdapter(),
        templates: {
            header: '<h3>Publikationen</h3>',
            empty: [
                '<div class="empty-message">',
                'Es konnten keine Publikationen gefunden werden',
                '</div>'
            ].join('\n'),
        }
    }
    , {
        name: 'authors',
        displayKey: 'DisplayText',
        // `ttAdapter` wraps the suggestion engine in an adapter that
        // is compatible with the typeahead jQuery plugin
        source: authors.ttAdapter(),
        templates: {
            header: '<h3>Autoren</h3>',
            empty: [
                '<div class="empty-message">',
                'Es konnten keine Autoren gefunden werden',
                '</div>'
            ].join('\n'),
        }
    }
    ).on('typeahead:selected', function (obj, datum) {
        
        getAllItems(datum);
    });
});



function getAllItems(datum) {
    
    switch(datum.SearchResultSourceType) {
        case 0:
            getAllPublicationsForAuthor(datum.DisplayText);
            $('.AuthorKnockoutTable').show();
            $('.ConferenceKnockoutTable').hide();
            $('.PublicationKnockoutTable').hide();
            break;
        case 1:
            getAllConferences(datum.Key);
            $('.AuthorKnockoutTable').hide();
            $('.ConferenceKnockoutTable').show();
            $('.PublicationKnockoutTable').hide();
            break;
        case 2:
            getAllPublications(datum.Key);
            $('.AuthorKnockoutTable').hide();
            $('.ConferenceKnockoutTable').hide();
            $('.PublicationKnockoutTable').show();
            break;
        default:
            alert('Unknown SearchResultType');
    }
}

var model = {
    Title: ko.observable(),
    authorModel: {
        AuthorName: ko.observable(),
        Publications: ko.observableArray(),
        selected: ko.observable(false)
    },
    conferenceModel: {
        ConferenceTitle: ko.observable(),
        Key: ko.observable(),
        Events: ko.observableArray(),

        ShowAllPublicationsFor: function (currentItem) {
            currentItem.ShowExtended(true);
            return false;
        },
        ShowNoPublicationsFor: function (currentItem) {
            currentItem.ShowExtended(false);
            return true;
        }
    },
    publicationModel: {
        PublicationTitle: ko.observable(),
        Key: ko.observable(),
        selected: ko.observable(false)
    }
};

function sendAjaxRequest(httpMethod, callback, url) {
    $.ajax("/api/Conference/GetConference" + (url ? "/" + url : "emptyValue"), { type: httpMethod, success: callback });
}

function sendAjaxRequestPublication(httpMethod, callback, url) {
    $.ajax("/api/Publication/GetPublicationSearchResult" + (url ? "/" + url : "emptyValue"), { type: httpMethod, success: callback });
}

function sendAjaxRequestAuthor(httpMethod, callback, url) {
    $.ajax("/api/Author/GetAuthorSearchResult" + (url ? "/" + url : "emptyValue"), { type: httpMethod, success: callback });
}

function getAllPublications(key) {
    sendAjaxRequestPublication("GET", function (data) {
        model.Title(data.PublicationName);
        model.publicationModel.PublicationTitle(data.Publication.DisplayText);
        model.publicationModel.Key(data.Publication.Key);
    }, key);
}

function getAllPublicationsForAuthor(key) {
    sendAjaxRequestAuthor("GET", function (data) {
        model.Title(data.AuthorName);
        model.authorModel.AuthorName(data.AuthorName);
        model.authorModel.Publications.removeAll();
        for (var i = 0; i < data.Publications.length; i++) {
            model.authorModel.Publications.push(
                {
                    Key: ko.observable(data.Publications[i].Key),
                    selected: ko.observable(false),
                    Title: data.Publications[i].DisplayText
                });
            
        }
    }, key);
}

function getAllConferences(key) {
    sendAjaxRequest("GET", function (data) {
        model.Title(data.ConferenceTitle);
        model.conferenceModel.ConferenceTitle(data.ConferenceTitle);
        model.conferenceModel.Key(data.Key);


        model.conferenceModel.Events.removeAll();
        for (var i = 0; i < data.Events.length; i++) {
            var publicationsInSubConference = ko.observableArray();
            if (data.Events[i].Publications != null) {
                for (var j = 0; j < data.Events[i].Publications.length; j++) {

                    var currentPublication = data.Events[i].Publications[j];

                    publicationsInSubConference.push(
                    {
                        Key: ko.observable(currentPublication.Key),
                        Title: currentPublication.Title,
                        Authors: currentPublication.Authors,
                        selected: ko.observable(false)
                    });
                }
                model.conferenceModel.Events.push(
                {
                    Key: ko.observable(data.Events[i].Key),
                    selected: ko.observable(false),
                    Title: data.Events[i].Title,
                    Publications: publicationsInSubConference,
                    ShowExtended: ko.observable(false)
                });
            }
        }
    }, key);
}

function toggleCheckbox() {
    var self = this;
    if (self.selected() == true) {
        RemoveItemFromCart(self);
    } else {
        AddItemToCart(self);
    }
    return true;
}

function AddItemToCart(self) {
    $.ajax("/ShoppingCart/AddKey/" + self.Key(), {
        type: "POST",
        success: function () {
            self.selected(true);
        }
    });
    return true;
}

function RemoveItemFromCart(self) {
    $.ajax("/ShoppingCart/DeleteKey/" + self.Key(), {
        type: "DELETE",
        success: function () {
            self.selected(false);
        }
    });
    return true;
}

$(document).ready(function () {
    $('.AuthorKnockoutTable').hide();
    $('.ConferenceKnockoutTable').hide();
    $('.PublicationKnockoutTable').hide();
    ko.applyBindings(model);
    $("#spinner").hide();
});

var callCounter = 0;

//Call method to display spinner
$(document).ajaxSend(function (event, jqXHR, settings) {
    callCounter++;
    $("#spinner").show();
});

//Call method to hide spinner
$(document).ajaxComplete(function (event, jqXHR, settings) {
    callCounter--;
    if (callCounter == 0) {
        $("#spinner").hide();
    }
});