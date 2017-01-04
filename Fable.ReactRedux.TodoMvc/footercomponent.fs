module FooterComponent

open Types

module R = Fable.Helpers.React

let createFooter _ = 
    R.p [] [
        R.text [] [ R.str "Show: "]
        (LinkComponent.createFilterLink Visibility.All [ R.str "All" ]) ()
        R.text [] [ R.str " " ]
        (LinkComponent.createFilterLink Visibility.Active [ R.str "Active" ]) ()
        R.text [] [ R.str " " ]
        (LinkComponent.createFilterLink Visibility.Completed [ R.str "Completed" ]) ()
    ]
