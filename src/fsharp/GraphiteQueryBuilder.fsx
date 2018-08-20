type Alias       = string
type Query       = string 
type TimeUnit    = Minutes of int
type SummariseBy = Average of TimeUnit
type Delta       = Delta of int

let alias (alias:Alias) (query:Query): Query = 
    sprintf "alias(%s, \"%s\")" query alias

let averageSeries (query:Query): Query = 
     sprintf "averageSeries(%s)" query

let diffSeries (source:Query) (diffWith:Query): Query =
    sprintf "diffSeries(%s,%s)" source diffWith

let holtWintersConfidenceBands (delta:Delta) (query:Query): Query = 
    match delta with 
    | Delta(d) -> sprintf "holtWintersConfidenceBands(%s, %i)" query d
    
let maxSeries (query:Query): Query = 
    sprintf "maxSeries(%s)" query       

let minSeries (query:Query): Query =
    sprintf "minSeries(%s)" query

let removeBelowValue (value:int) (query:Query): Query =
    sprintf "removeBelowValue(%s, %i)" query value

let summarise (summariseBy:SummariseBy) (query:Query): Query =
    match summariseBy with 
    | Average(timeUnit) ->
        match timeUnit with
        | Minutes(minutes) -> sprintf "summarize(%s, \"%iminutes\", \"avg\")" query minutes

let transformNull (value:int) (query:Query): Query =
    sprintf "transformNull(%s, %i)" query value
