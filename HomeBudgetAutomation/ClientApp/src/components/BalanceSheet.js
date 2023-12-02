import React, { Component } from 'react';
import { Nav, NavItem, NavLink, ButtonGroup, Button, Alert } from 'reactstrap';
import { Link } from 'react-router-dom';
import authService from './api-authorization/AuthorizeService'

const URL = 'api/balances'

export class BalanceSheet extends Component {
  static displayName = BalanceSheet.name;

  constructor(props) {
    super(props);
    this.state = { balances: [], loading: true, error: false, errorMessage: "sdfds" };
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
            <th>Дата формирования</th>
            <th>Доход</th>
            <th>Расход</th>
            <th>Прибыль</th>
            <th style={{textAlign: 'right'}}>Действия</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td></td>
            <td style={{textAlign: 'left'}}>
              <input id='name' type="text" />
            </td>
            <td></td>
            <td></td>
            <td></td>
            <td style={{textAlign: 'right'}}>
              <button className="btn btn-primary btn-block" onClick={() => this.post()}>Сформировать</button>
            </td>
          </tr>
          {this.state.balances.map(balance =>
            <tr key={balance.id}>
              <td>{balance.id}</td>
              <td>{balance.name}</td>
              <td></td>
              <td></td>
              <td></td>
              <td style={{textAlign: 'right'}}>
                <ButtonGroup aria-label="Basic example">
                  <Button color="danger" onClick={() => this.deleteById(balance.id)}>Расформировать</Button>
                </ButtonGroup>
              </td>
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
        <h1 id="tabelLabel" >Справочник статей расхода</h1>
        <p>Здесь представлена таблица статей расхода, вы можете их создавать, удалять и редактировать</p>
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

    const balance = {
      name: document.querySelector('#name').value
    }

    if (balance.name.trim() == "") {
      this.setState({error: true, errorMessage: "Пустое имя недопустимо!"})
      return
    }
    
    const response = await fetch(URL, {
      method: 'POST',
      headers: headers,
      body: JSON.stringify(balance)
    });
    
    if (response.ok) {
      const data = await response.json()
      this.state.balances.push(data)
      this.setState({balances: this.state.balances, loading: false, error: false})
    }
  }

  async getAll() {
    const token = await authService.getAccessToken();
    const response = await fetch(URL, {
      headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
    });
    const data = await response.json();
    this.setState({ balances: data, loading: false, error: false });
  }

  async deleteById(id) {
    const token = await authService.getAccessToken();
    try {
      const response = await fetch(URL + `/${id}`, {
        method: 'DELETE',
        headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
      });
      if (response.ok) {
        const list = this.state.balances.filter(x => x.id !== id)
        this.setState({ balances: list, loading: false, error: false })
      } else {
        throw new Error()
      }
    } catch {
      this.setState({error: true, errorMessage: "Статья содержит закрытые операции!"})
    }
    
  }
}
