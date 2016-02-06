# CoffeeScript
$ ->
    $('#NewTag').typeahead
        name: 'Tags'
        source: (query, process) ->
            tags = []
            $.getJSON '/Home/AutoCompleteTag', { term: query }, (data) ->
                $.each data, (i, val) ->
                    tags.push val
                    return
                process tags
    return