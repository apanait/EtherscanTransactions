import axios from 'axios';
const apiBase = "api";
const headers = () => ({
    "Accept": "application/json",
    "Content-Type": "application/json"
});

const getTransactions = async () => {
    const url = `${apiBase}/transactions`;
    try{
        return await fetchGet(url, null, headers());
    }
    catch(e)
    {
        console.log('err:', e);
    }
}

const saveTransactions = async (data) => {
    const url = `${apiBase}/transactions`;
    try{
        return await fetchPost(url, data, headers());
    }
    catch(e)
    {
        console.log('err:', e);
    }
}
const fetchPost = async (url, data, headers, responseType) => {
    let result = axios.post(url, data, { headers: { ...headers }, responseType: responseType });
    return result;
}
const fetchGet = async (url, data, headers) => {
    let result = "";
    if (data) {
        result = await axios.get(url, { params: { ...data }, headers: { ...headers } });
    }
    else {
        result = await axios.get(url, { headers: { ...headers } });
    }
    return result;
}

export default{
    getTransactions,
    saveTransactions
}