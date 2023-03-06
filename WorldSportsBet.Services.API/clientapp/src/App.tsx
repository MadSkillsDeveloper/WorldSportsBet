import { useState, useEffect } from 'react';
import reactLogo from './assets/react.svg';
import viteLogo from './assets/vite.svg';
import './App.css'

import * as signalR from '@microsoft/signalr';
import Chart from './Components/chart/chart';

function App() {
const [data, setData] = useState();
const chartState: any = {
  data: data
};

const chartProps: any = {
  title: "",
    xName: "",
    yName: "",
    type: "",
    seriesName: ""
};


useEffect(() => {
  const connection = new signalR
    .HubConnectionBuilder()
      .withUrl("/CurrencyRate")
      .build();

  connection.on("ReceiveData", (response) =>{
    console.log(response);
    setData(response);
  });
}, [])

  return (
    <>
    <div className='layout grid grid-rows-3 gap-4'>
      <div className='wsb-header p-5'>
        <a href='https://www.worldsportsbetting.co.za/'>
          <img src="https://cdn.worldsportsbetting.co.za/images/WSBWC/World-Sports-Betting.png" 
                alt='World Sports Betting' 
                width="104" 
                height="45"/>
        </a>
      </div>
      <div className='contentBody'>
        <Chart {...chartProps} {...chartState}/>
      </div>
      <div>footer</div>
    </div>
    </>
  );
}

export default App
