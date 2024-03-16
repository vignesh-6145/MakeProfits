const getClients = async (AdvisorID) => {
    try{
        const clients = await axios.get(`Advisor/${AdvisorID}/Clients`);
        return clients;
    }
    catch(e){
        const msg = e?.response?.error.message ?? e?.message ?? 'Unknown Error';
        console.error(msg);
        return false;
    }
};

export default getClients;