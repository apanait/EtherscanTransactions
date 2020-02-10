import React, { Component } from 'react';
import  apis from '../apis'

export class Transactions extends Component {
  static displayName = Transactions.name;
    apiPath = 'api/saveTransactions';
    transactionList = 'none';

  constructor(props) {
      super(props);
      this.state = { transactions: [] };
    }

    getTransactions = async () => {
        var response = await apis.getTransactions();
        console.log(response.data.result.length);
        if (response !== undefined && response.data !== undefined && response.data.result !== undefined) {
           this.setState({ transactions: response.data.result });
        }
  }

  saveTransactions = () => {
  }
    render() {
       
    return (
      <div>
        <div className="container">
          <div className="row">
            <div className="col-sm-6">
              <button className="btn btn-primary" onClick={this.getTransactions}>GetTransactions</button>
            </div>
            <div className="col-sm-6">
              <button className="btn btn-primary" onClick={this.saveTransactions}>SaveTransactions</button>
            </div>
          </div>
                <div className="row">
                    <div className="col-xs-12">
                        <ul>{this.state.transactions.length > 0 ? this.state.transactions.map((item, i) => <li key={i}>{item.data} | {item.address} | {item.transactionHash}</li>) : ''} </ul>
                    </div>
          </div>
        </div>
      </div>
    );
  }
}
