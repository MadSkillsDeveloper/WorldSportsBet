import { 
    ChartComponent, 
    SeriesCollectionDirective, 
    SeriesDirective, 
    Inject, 
    Legend, 
    Tooltip } 
    from '@syncfusion/ej2-react-charts';


type ChartProps = {
    title: string,
    xName: string,
    yName: string,
    type: string,
    seriesName: string
}

type ChartState = {
    data:any
}

const Chart =(chartProps: ChartProps, chartState: ChartState) => {
    const {data} = chartState;

    
    return(
        <>
        <ChartComponent title='Currency Rates'>
            <Inject services={[Legend, Tooltip]}/>
            <SeriesCollectionDirective>
                <SeriesDirective dataSource={data} 
                                    xName="currency" 
                                    yName='time' 
                                    type='Column'
                                    name='Currency'/>
            </SeriesCollectionDirective>
        </ChartComponent>
        </>
    );
}

export default Chart;