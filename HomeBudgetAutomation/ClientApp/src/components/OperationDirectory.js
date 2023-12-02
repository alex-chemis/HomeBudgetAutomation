import React, { Component } from 'react';
import { useParams } from 'react-router-dom';
import { ButtonGroup, Button, Alert } from 'reactstrap';
import { Link } from 'react-router-dom';
import authService from './api-authorization/AuthorizeService'

const URL = 'api/operations'

export class OperationDirectory extends Component {
  static displayName = OperationDirectory.name;

  constructor(props) {
    super(props);
    this.state = { operations: [], loading: true, error: false, errorMessage: "sdfds" };
  }

  componentDidMount() {
    this.getAll();
  }

  renderTable() {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>ID</th>
            <th>Доход</th>
            <th>Убыток</th>
            <th>Дата создания</th>
            <th>Баланс</th>
            <th style={{textAlign: 'right'}}>Действия</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td></td>
            <td style={{textAlign: 'left'}}>
              <input id='debit' type="text" />
            </td>
            <td style={{textAlign: 'left'}}>
              <input id='credit' type="text" />
            </td>
            <td style={{textAlign: 'left'}}>
              <input id='createDate' type="text" />
            </td>
            <td></td>
            <td style={{textAlign: 'right'}}>
              <button className="btn btn-primary btn-block" onClick={() => this.post()}>Добавить</button>
            </td>
          </tr>
          {this.state.operations.map(operation => operation.balanceId == null 
            ?
              <tr key={operation.id}>
                <td>{operation.id}</td>
                <td>{operation.debit}</td>
                <td>{operation.credit}</td>
                <td>{operation.createDate}</td>
                <td>Не учтена</td>
                <td style={{textAlign: 'right'}}>
                  <ButtonGroup aria-label="Basic example">
                    <Button color="warning" onClick={() => this.updateById(operation.id)}>Обновить</Button>
                    <Button color="danger" onClick={() => this.deleteById(operation.id)}>Удалить</Button>
                  </ButtonGroup>
                </td>
              </tr>
            :
              <tr class="table-danger" key={operation.id}>
                <td>{operation.id}</td>
                <td>{operation.debit}</td>
                <td>{operation.credit}</td>
                <td>{operation.createDate}</td>
                <td>{operation.balanceId}</td>
                <td></td>
              </tr>
          )}
          
        </tbody>
        <tfoot>
          
        </tfoot>
      </table>
    );
  }

  render() {
    let error = this.state.error
      ?
        <Alert color="danger">
          {this.state.errorMessage}
        </Alert>
      : <></>

    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : this.renderTable();

    return (
      <div>
        <h1 id="tabelLabel" >{`Справочник операций по статье ${this.props.articleId}`}</h1>
        <p>Здесь представлена таблица операций, вы можете их создавать, удалять и редактировать</p>
        {error}
        {contents}
      </div>
    );
  }

  async post() {
    const token = await authService.getAccessToken();

    const headers = new Headers()
    headers.set('Content-Type', 'application/json')

    if (token) {
      headers.set('Authorization', `Bearer ${token}`)
    }

    const operation = {
      articleId: this.props.articleId,
      debit: parseInt(document.querySelector('#debit').value),
      credit: parseInt(document.querySelector('#credit').value),
      createDate: new Date(document.querySelector("#createDate").value)
    }

    if (isNaN(operation.debit) || operation.debit == 0) {
      this.setState({error: true, errorMessage: "Доход задан некорректно!"})
      return
    }

    if (isNaN(operation.debit) == NaN || operation.debit == 0) {
      this.setState({error: true, errorMessage: "Расход задан некорректно!"})
      return
    }

    if (isNaN(operation.createDate)) {
      this.setState({error: true, errorMessage: "Дата задана некорректно!"})
      return
    }
    
    const response = await fetch(URL, {
      method: 'POST',
      headers: headers,
      body: JSON.stringify(operation)
    });
    
    if (response.ok) {
      const data = await response.json()
      this.state.operations.push(data)
      this.setState({operations: this.state.operations, loading: false, error: false})
    }
  }

  async getAll() {
    const token = await authService.getAccessToken();
    const response = await fetch(URL, {
      headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
    });
    const data = await response.json();
    this.setState({ operations: data.filter(x => x.articleId == this.props.articleId), loading: false, error: false });
  }

  async updateById(id) {
    const token = await authService.getAccessToken();

    const headers = new Headers()
    headers.set('Content-Type', 'application/json')

    if (token) {
      headers.set('Authorization', `Bearer ${token}`)
    }

    const operation = {
      articleId: this.props.articleId,
      debit: parseInt(document.querySelector('#debit').value),
      credit: parseInt(document.querySelector('#credit').value),
      createDate: new Date(document.querySelector("#createDate").value)
    }

    if (isNaN(operation.debit) || operation.debit == 0) {
      this.setState({error: true, errorMessage: "Доход задан некорректно!"})
      return
    }

    if (isNaN(operation.debit) == NaN || operation.debit == 0) {
      this.setState({error: true, errorMessage: "Расход задан некорректно!"})
      return
    }

    if (isNaN(operation.createDate)) {
      this.setState({error: true, errorMessage: "Дата задана некорректно!"})
      return
    }

    const response = await fetch(URL + `/${id}`, {
      method: 'PATCH',
      headers: headers,
      body: JSON.stringify(operation)
    });
    
    if (response.ok) {
      const data = await response.json()
      const list = this.state.operations.map(operation => operation.id !== id ? operation : data)
      this.setState({operations: list, loading: false, error: false})
    }
  }

  async deleteById(id) {
    const token = await authService.getAccessToken();
    const response = await fetch(URL + `/${id}`, {
      method: 'DELETE',
      headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
    });
    if (response.ok) {
      const list = this.state.operations.filter(x => x.id !== id)
      this.setState({ operations: list, loading: false, error: false })
    }
  }
}

const Ops = () => {
    const { id } = useParams();
    console.log(id);

    return (
        <OperationDirectory articleId={id}/>
    )
}

export default Ops;