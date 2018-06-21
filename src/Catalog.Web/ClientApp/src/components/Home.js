import React, {Component} from 'react';
import {connect} from 'react-redux';
import {bindActionCreators} from 'redux';
import {actionCreators} from '../store/Products';
import { Button, ButtonGroup, Glyphicon, Table, Grid, Row,
    Form, FormGroup, FormControl, ControlLabel } from 'react-bootstrap';

class Home extends Component {
    
    state = {}
    
    componentWillMount() {
        const term = '';
        this.props.requestProducts(term);
    }

    handleRemoveProduct = (product) => () => {
        this.props.removeProduct(product.id);
    };

    handleUpdateProduct = (product) => () => {
        this.props.updateProduct({
            id: product.id,
            code: this.state.code,
            name: this.state.name,
            price: this.state.price,
        });
    };
    
    handleAddProduct = () => {
        this.props.addProduct({
            code: this.state.code,
            name: this.state.name,
            price: this.state.price,
        });
    };
    
    handleInput = name => ev => {
        console.log(ev.target.value);
        this.setState({
            [name]: ev.target.value,
        })
    };

    renderDetails() {
        return (
            <Form inline>
                <FormGroup controlId="formCode">
                    <FormControl type="text" placeholder="code"
                                 value={this.state.code} onChange={this.handleInput('code')} />
                </FormGroup>{' '}
                <FormGroup controlId="formName">
                    <ControlLabel>Name</ControlLabel>{' '}
                    <FormControl type="text" placeholder="name"
                                 value={this.state.name} onChange={this.handleInput('name')} />
                </FormGroup>{' '}
                <FormGroup controlId="formPrice">
                    <ControlLabel>Price</ControlLabel>{' '}
                    <FormControl type="text" placeholder="100.00"
                                 value={this.state.price} onChange={this.handleInput('price')} />
                </FormGroup>{' '}
                <Button onClick={this.handleAddProduct}>Add Product</Button>
            </Form>
        );
    }

    renderTable() {
        return (
            <Table striped bordered condensed hover>
                <thead>
                <tr>
                    <th>Id</th>
                    <th>Code</th>
                    <th>Name</th>
                    <th>Price</th>
                    <th>Updated</th>
                    <th>&nbsp;</th>
                </tr>
                </thead>
                <tbody>
                {this.props.products.map(product =>
                    <tr key={product.id}>
                        <td>{product.id}</td>
                        <td>{product.code}</td>
                        <td>{product.name}</td>
                        <td>{product.price}</td>
                        {/*<td>{product.lastUpdated}</td>*/}
                        <td>
                            <ButtonGroup>
                                <Button onClick={this.handleUpdateProduct(product)}>
                                    <Glyphicon glyph="pencil"/>
                                </Button>
                                <Button onClick={this.handleRemoveProduct(product)}>
                                    <Glyphicon glyph="trash"/>
                                </Button>
                            </ButtonGroup>
                        </td>
                    </tr>
                )}
                </tbody>
            </Table>
        );
    }
    
    render() {
        return (
            <Grid>
                <Row>
                    <h1>Products</h1>
                </Row>
                <Row>
                    {this.renderTable()}
                </Row>
                <Row>
                    {this.renderDetails()}
                </Row>
            </Grid>
        );
    }
}

export default connect(
    state => state.products,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(Home);
