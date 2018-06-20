import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { actionCreators } from '../store/Products';

class Home extends Component {
    componentWillMount() {
        const term = 'p';
        this.props.requestProducts(term);
    }

    render() {
        return (
            <div>
                <h1>Products</h1>
                {renderForecastsTable(this.props)}
            </div>
        );
    }
}

function renderForecastsTable(props) {
    return (
        <table className='table'>
            <thead>
            <tr>
                <th>Id</th>
                <th>Code</th>
                <th>Name</th>
                <th>Price</th>
                <th>Updated</th>
            </tr>
            </thead>
            <tbody>
            {props.products.map(product =>
                <tr key={product.id}>
                    <td>{product.id}</td>
                    <td>{product.code}</td>
                    <td>{product.name}</td>
                    <td>{product.price}</td>
                    <td>{product.lastUpdated}</td>
                </tr>
            )}
            </tbody>
        </table>
    );
}

export default connect(
    state => state.products,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(Home);
