const requestProductsType = 'REQUEST_PRODUCTS';
const receiveProductsType = 'RECEIVE_PRODUCTS';
const removeProductType = 'REMOVE_PRODUCT';
const addProductType = 'ADD_PRODUCT';

const initialState = {products: [], isLoading: false};

export const actionCreators = {
    requestProducts: term => async (dispatch, getState) => {
        if (term === getState().products.term) {
            // Don't issue a duplicate request (we already have or are loading the requested data)
            return;
        }

        dispatch({type: requestProductsType, term});

        const url = `/api/v1/products?term=${term}`;
        const response = await fetch(url);
        const products = await response.json();

        dispatch({type: receiveProductsType, term, products});
    },

    removeProduct: productId => async (dispatch, getState) => {
        dispatch({type: removeProductType, id: productId});

        let url = `/api/v1/products/${productId}`;
        await fetch(url, {
            method: 'DELETE',
        });

        const term = getState().products.term;
        url = `/api/v1/products?term=${term}`;
        const response = await fetch(url);
        const products = await response.json();

        dispatch({type: receiveProductsType, term, products});
    },

    addProduct: product => async (dispatch, getState) => {
        dispatch({type: addProductType, product});

        let url = '/api/v1/products/';
        await fetch(url, {
            method: 'POST',
            cache: 'no-cache',
            headers: {
                'content-type': 'application/json',
                'accept': 'application/json',
            },
            body: JSON.stringify(product),
        });

        const term = getState().products.term;
        url = `/api/v1/products?term=${term}`;
        const response = await fetch(url);
        const products = await response.json();

        dispatch({type: receiveProductsType, term, products});
    },
};

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === requestProductsType) {
        return {
            ...state,
            term: action.term,
            isLoading: true,
        };
    }

    if (action.type === receiveProductsType) {
        return {
            ...state,
            term: action.term,
            products: action.products,
            isLoading: false,
        };
    }

    if (action.type === removeProductType) {
        return {
            ...state,
            products: state.products.filter(x => x.id !== action.id)
        };
    }

    return state;
};
