module FooterComponent

open Types

module R = Fable.Helpers.React

let createFooter _ = 
    R.p [] [
        R.text [] [ unbox "Show: "]
        LinkComponent.createFilterLink Visibility.All [ unbox "All" ]
        R.text [] [ unbox " " ]
        LinkComponent.createFilterLink Visibility.Active [ unbox "Active" ]
        R.text [] [ unbox " " ]
        LinkComponent.createFilterLink Visibility.Completed [ unbox "Completed" ]
    ]
