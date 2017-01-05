module FooterComponent

open Types

module R = Fable.Helpers.React

let private allVisibilityLink =
    LinkComponent.createFilterLink Visibility.All [ R.str "All" ]

let private activeVisibilityLink =
    LinkComponent.createFilterLink Visibility.Active [ R.str "Active" ]

let private completedVisibilityLink =
    LinkComponent.createFilterLink Visibility.Completed [ R.str "Completed" ]

let createFooter _ = 
    R.p [] [
        R.text [] [ R.str "Show: "]
        allVisibilityLink ()
        R.text [] [ R.str " " ]
        activeVisibilityLink ()
        R.text [] [ R.str " " ]
        completedVisibilityLink ()
    ]
