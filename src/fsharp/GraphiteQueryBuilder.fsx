open Microsoft.FSharp.Reflection
open System.Text

type Alias         = string
type Query         = string 
type Tag           = string
type SearchString  = string
type ReplaceString = string
type Color         = string
type Position      = int
type Node          = int
type Alpha         = float
type TimeUnit      = Minutes of int
type SummariseBy   = AverageBy of TimeUnit
type Delta         = Delta of int
type Aggregation   = | Average | Median | Sum | Min | Max | Diff
                     | StdDev | Count | Range | Multiply | Last
type Consolidation = | Sum | Average | Min | Max | First | Last


let private unionToString (x:'a) =
    match FSharpValue.GetUnionFields(x, typeof<'a>) with
    | case, _ -> case.Name

let private lower (x:string) = x.ToLower()



let absolute (query:Query): Query =
    sprintf "absolute(%s)" query

let aggregate (aggregation:Aggregation) (query:Query) =
    let agg = aggregation |> unionToString |> lower 
    sprintf "aggregate(%s,\"%s\")" query agg

let aggregateLine (aggregation:Aggregation) (query:Query) =
    let agg = aggregation |> unionToString |> lower 
    sprintf "aggregateLine(%s,\"%s\")" query agg

let aggregateWithWildcards (aggregation:Aggregation) (query:Query) (position:Position) =
    let agg = aggregation |> unionToString |> lower 
    sprintf "aggregateWithWildcards(%s,\"%s\",%i)" query agg position

let alias (alias:Alias) (query:Query): Query = 
    sprintf "alias(%s,\"%s\")" query alias

let aliasByMetric (query:Query): Query = 
    sprintf "aliasByMetric(%s)" query

let aliasByNode (node:Node) (query:Query): Query = 
    sprintf "aliasByNode(%s,%i)" query node

let aliasByTags (tag:Tag) (query:Query): Query = 
    sprintf "aliasByTags(%s,\"%s\")" query tag

let aliasQuery (search:SearchString) (replace:ReplaceString) (newName:Alias) (query:Query): Query = 
    sprintf "aliasQuery(%s,\"%s\",\"%s\",\"%s\")" query search replace newName

let aliasSub (search:SearchString) (replace:ReplaceString) (query:Query): Query = 
    sprintf "aliasSub(%s,\"%s\",\"%s\")" query search replace

let alpha (query:Query) (alpha:Alpha): Query =
    if alpha < 0.0 || alpha > 1.0 then
        failwith "Alpha value must be between 0.0 and 1.0"
    else
        sprintf "alpha(%s,%f)" query alpha            

let applyByNode (node:Node) (templateQuery:Query) (query:Query): Query =
    sprintf "applyByNode(%s,%i,\"%s\")" query node templateQuery

let areaBetween(query:Query): Query =
    sprintf "areaBetween(%s)" query

let asPercent (query:Query): Query =
    failwith "Need to update the domain model to support this"
    // https://graphite.readthedocs.io/en/latest/functions.html#graphite.render.functions.asPercent

let averageAbove (value:int) (query:Query): Query =
    sprintf "averageAbove(%s,%i)" query value

let averageBelow (value:int) (query:Query): Query =
    sprintf "averageBelow(%s,%i)" query value

let averageOutsidePercentile (value:int) (query:Query): Query =
    sprintf "averageOutsidePercentile(%s,%i)" query value

let averageSeries (query:Query): Query = 
     sprintf "averageSeries(%s)" query

let averageSeriesWithWildcards (position:Position) (query:Query): Query = 
     sprintf "averageSeriesWithWildcards(%s,%i)" query position

let cactiStyle (query:Query): Query =
    failwith "Need to update the domain model to support this"
    // https://graphite.readthedocs.io/en/latest/functions.html#graphite.render.functions.cactiStyle

let changed (query:Query): Query =
    sprintf "changed(%s)" query

let color (color:Color) (query:Query): Query = 
    sprintf "color(%s,'%s')" query color

let consolidateBy (consolidation:Consolidation) (query:Query): Query =
    let con = consolidation |> unionToString |> lower 
    sprintf "consolidateBy(%s,'%s')" query con

let constantLine (position:float): Query =
    sprintf "constantLine(%f)" position

let countSeries (query:Query): Query =
    sprintf "countSeries(%s)" query

let cumulative (query:Query): Query =
    sprintf "cumulative(%s)" query

let currentAbove (value:int) (query:Query): Query =
    sprintf "currentAbove(%s,%i)" query value

let currentBelow (value:int) (query:Query): Query =
    sprintf "currentBelow(%s,%i)" query value



// ----------------------------
// Next to Implement: https://graphite.readthedocs.io/en/latest/functions.html#graphite.render.functions.dashed
// ----------------------------



let diffSeries (source:Query) (diffWith:Query): Query =
    sprintf "diffSeries(%s,%s)" source diffWith

let holtWintersConfidenceBands (delta:Delta) (query:Query): Query = 
    match delta with 
    | Delta(d) -> sprintf "holtWintersConfidenceBands(%s,%i)" query d
    
let maxSeries (query:Query): Query = 
    sprintf "maxSeries(%s)" query       

let minSeries (query:Query): Query =
    sprintf "minSeries(%s)" query

let removeBelowValue (value:int) (query:Query): Query =
    sprintf "removeBelowValue(%s,%i)" query value

let summarise (summariseBy:SummariseBy) (query:Query): Query =
    match summariseBy with 
    | AverageBy(timeUnit) ->
        match timeUnit with
        | Minutes(minutes) -> sprintf "summarize(%s,\"%iminutes\",\"avg\")" query minutes

let transformNull (value:int) (query:Query): Query =
    sprintf "transformNull(%s,%i)" query value
