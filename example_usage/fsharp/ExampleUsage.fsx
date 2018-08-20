#load "../../src/fsharp/GraphiteQueryBuilder.fsx"

open GraphiteQueryBuilder

"api.myservice.cpu"
|> transformNull 0
|> averageSeries
|> alias "Average CPU"