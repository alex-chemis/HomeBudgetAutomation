import React, { Component } from 'react';
import { ButtonGroup, Button, Alert } from 'reactstrap';
import authService from './api-authorization/AuthorizeService'
import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from 'recharts';

const URL = 'api/balances'

const data1 = [
  {
    name: 'Page A',
    uv: 4000,
    pv: 2400,
    amt: 2400,
  },
  {
    name: 'Page B',
    uv: 3000,
    pv: 1398,
    amt: 2210,
  },
  {
    name: 'Page C',
    uv: 2000,
    pv: 9800,
    amt: 2290,
  },
  {
    name: 'Page D',
    uv: 2780,
    pv: 3908,
    amt: 2000,
  },
  {
    name: 'Page E',
    uv: 1890,
    pv: 4800,
    amt: 2181,
  },
  {
    name: 'Page F',
    uv: 2390,
    pv: 3800,
    amt: 2500,
  },
  {
    name: 'Page G',
    uv: 3490,
    pv: 4300,
    amt: 2100,
  },
];

export class Report extends Component {
  static displayName = Report.name;

  constructor(props) {
    super(props);
    this.state = { operations: [], articles: [], balances: [], loading: true, error: false, errorMessage: "sdfds" };
  }

  componentDidMount() {
    this.getAllBalance();
  }


  renderInputOpTable() {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Дата начало анализа</th>
            <th>Дата конца анализа</th>
            <th>Анализируемые статьи</th>
            <th>Действие</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td style={{textAlign: 'left'}}>
              <input id='start_date1' type="text" />
            </td>
            <td style={{textAlign: 'left'}}>
              <input id='end_date1' type="text" />
            </td>
            <td style={{textAlign: 'left'}}>
              <input id='article_ids1' type="text" />
            </td>
            <td style={{textAlign: 'right'}}>
              <button className="btn btn-primary btn-block" onClick={() => this.getAllOperations()}>Анализ</button>
            </td>
          </tr>
        </tbody>
      </table>
    )
  }

  renderInputPrTable() {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Дата начало анализа</th>
            <th>Дата конца анализа</th>
            <th>Анализируемые статьи</th>
            <th>Тип анализа</th>
            <th>Действие</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td style={{textAlign: 'left'}}>
              <input id='start_date2' type="text" />
            </td>
            <td style={{textAlign: 'left'}}>
              <input id='end_date2' type="text" />
            </td>
            <td style={{textAlign: 'left'}}>
              <input id='article_ids2' type="text" />
            </td>
            <td>
              <select id='flowType' type="text">
                <option>debit</option>
                <option>credit</option>
                <option>amount</option>
              </select>
            </td>
            <td style={{textAlign: 'right'}}>
              <button className="btn btn-primary btn-block" onClick={() => this.getAllPr()}>Анализ</button>
            </td>
          </tr>
        </tbody>
      </table>
    )
  }

  renderOperationTable() {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>ID</th>
            <th>ID статьи</th>
            <th>Доход</th>
            <th>Убыток</th>
            <th>Дата создания</th>
          </tr>
        </thead>
        <tbody>
          {this.state.operations.map(operation =>
              <tr key={operation.id}>
                <td>{operation.id}</td>
                <td>{operation.articleId}</td>
                <td>{operation.debit}</td>
                <td>{operation.credit}</td>
                <td>{operation.createDate}</td>
              </tr>
          )}
          
        </tbody>
        <tfoot>
          
        </tfoot>
      </table>
    );
  }

  renderPrTable() {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>ID статьи</th>
            <th>процент</th>
          </tr>
        </thead>
        <tbody>
          {this.state.articles.map(article =>
              <tr key={article.id}>
                <td>{article.id}</td>
                <td>{article.percentage}</td>
              </tr>
          )}
        </tbody>
        <tfoot>
          
        </tfoot>
      </table>
    );
  }

  renderGraph() {
    return (
        <LineChart
          width={500}
          height={300}
          data={this.state.balances.map(balance => ({
            date: balance.createDate,
            amount: balance.amount
          }))}
          margin={{
            top: 5,
            right: 30,
            left: 20,
            bottom: 5,
          }}
        >
          <CartesianGrid strokeDasharray="3 3" />
          <XAxis dataKey="date" />
          <YAxis />
          <Tooltip />
          <Legend />
          <Line type="monotone" dataKey="amount" stroke="#8884d8" activeDot={{ r: 8 }} />
        </LineChart>
    );
  }

  render() {
    let error = this.state.error
      ?
        <Alert color="danger">
          {this.state.errorMessage}
        </Alert>
      : <></>

    let contents = this.renderOperationTable();

    return (
      <div>
        <h1 id="tabelLabel" >Сводный отчет</h1>
        <p>Здесь представлена таблица операций за заданный период по заданным статьям</p>
        {error}
        {this.renderInputOpTable()}
        {contents}
        <p>Здесь представлен процент от дохода/расхода/прибыли по заданным статьям</p>
        {this.renderInputPrTable()}
        {this.renderPrTable()}
        <p>Здесь представлен график чисто прибыли баланса</p>
        {this.renderGraph()}
      </div>
    );
  }

  async getAllOperations() {
    const token = await authService.getAccessToken();

    const headers = new Headers()
    headers.set('Content-Type', 'application/json')

    if (token) {
      headers.set('Authorization', `Bearer ${token}`)
    }

    const operation = {
      startDate: new Date(document.querySelector('#start_date1').value),
      endDate: new Date(document.querySelector('#end_date1').value),
      articleIds: new String(document.querySelector('#article_ids1').value).split(',').map(id => parseInt(id))
    }

    if (isNaN(operation.startDate)) {
      this.setState({error: true, errorMessage: "Некорректна задана дата!"})
      return
    }

    if (isNaN(operation.endDate)) {
      this.setState({error: true, errorMessage: "Некорректна задана дата!"})
      return
    }
    
    try {
      const response = await fetch('/api/operations/stats', {
        method: 'PUT',
        headers: headers,
        body: JSON.stringify(operation)
      });
        
      if (response.ok) {
        const data = await response.json();
        this.setState({operations: data, loading: false, error: false})
      } else {
        throw new Error()
      }
    } catch {
      this.setState({error: true, errorMessage: "Некорректно задан массив"})
    }
  }

  async getAllPr() {
    const token = await authService.getAccessToken();

    const headers = new Headers()
    headers.set('Content-Type', 'application/json')

    if (token) {
      headers.set('Authorization', `Bearer ${token}`)
    }

    const operation = {
      startDate: new Date(document.querySelector('#start_date2').value),
      endDate: new Date(document.querySelector('#end_date2').value),
      articleIds: new String(document.querySelector('#article_ids2').value).split(',').map(id => parseInt(id)),
      flowType: document.querySelector('#flowType').value
    }

    if (isNaN(operation.startDate)) {
      this.setState({error: true, errorMessage: "Некорректна задана дата!"})
      return
    }

    operation.startDate = operation.startDate.toISOString().slice(0, -1)

    if (isNaN(operation.endDate)) {
      this.setState({error: true, errorMessage: "Некорректна задана дата!"})
      return
    }

    operation.endDate = operation.endDate.toISOString().slice(0, -1)
    
    try {
      const response = await fetch('/api/articles/percentage', {
        method: 'PUT',
        headers: headers,
        body: JSON.stringify(operation)
      });
        
      if (response.ok) {
        const data = await response.json();
        this.setState({articles: data, loading: false, error: false})
      } else {
        throw new Error()
      }
    } catch {
      this.setState({error: true, errorMessage: "Некорректно задан массив"})
    }
  }

  async getAllBalance() {
    const token = await authService.getAccessToken();
    const response = await fetch(URL, {
      headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
    });
    const data = await response.json();
    this.setState({ balances: data, loading: false, error: false });
  }
}
