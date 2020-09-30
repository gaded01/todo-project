import axios from 'axios';
import authHeader from './auth-header';


const API_URL = 'http://localhost:5001/';
const userToken = JSON.parse(localStorage.getItem('user'))


class UserService {
  
  getUserAll() {
    return axios.get(API_URL + 'users' , {headers: authHeader() });
  }


  getUserActivity(userId) {
    return axios.get(API_URL + 'activities/' + 'fetchActivity/' + userId, 
    {headers: authHeader()});
  }

  
  addNewActivity(userId,activity) { 
    const data = {
      eventActivity: activity.eventActivity,
      dateTimeStart: activity.dateTimeStart,
      dateTimeEnd: activity.dateTimeEnd,
      details: activity.details,
      setting: activity.setting
    }
    const headers = {  
        'Authorization': 'Bearer ' + userToken.token, 
        'Content-Type': 'application/json'
      };
    return axios.post(API_URL + 'activities/addActivity/' + userId, data, {headers});
  }



  deleteActivity(activityId)  {
    return axios.delete(API_URL + 'activities/deleteActivity/' + activityId, {  
    headers: authHeader()});
  }


  updateActivity(actId, activity)  {
    const updateData = {
      actId: activity.actId,
      eventActivity: activity.eventActivity,
      dateTimeStart: activity.dateTimeStart,
      dateTimeEnd: activity.dateTimeEnd,
      details: activity.details,
      setting: activity.setting
    }
    const headers = {  
        'Authorization': 'Bearer ' + userToken.token, 
        'Content-Type': 'application/json'
      };
    return axios.update(API_URL + 'activities/updateActivity/' + actId, updateData, {headers});
  }



}
export default new UserService();