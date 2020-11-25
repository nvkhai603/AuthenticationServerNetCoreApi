import { api } from './axios'
const RESOURCE = "groups";
export default {
    getAllGroups() {
        return api.get(RESOURCE)
    }
}