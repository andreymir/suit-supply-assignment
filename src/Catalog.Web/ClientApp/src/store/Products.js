const requestProductsType = 'REQUEST_PRODUCTS';
const receiveProductsType = 'RECEIVE_PRODUCTS';
const initialState = { products: [], isLoading: false };

export const actionCreators = {
  requestProducts: term => async (dispatch, getState) => {    
    if (term === getState().products.term) {
      // Don't issue a duplicate request (we already have or are loading the requested data)
      return;
    }

    dispatch({ type: requestProductsType, term });

    const url = `/api/v1/products?term=${term}`;
    const response = await fetch(url);
    const products = await response.json();

    dispatch({ type: receiveProductsType, term, products });
  }
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

  return state;
};
