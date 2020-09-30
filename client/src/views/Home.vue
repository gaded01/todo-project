<template>
  <div>
    <v-app>
      <v-main>
        <v-container
          id="container"
          class="fill-height"
          fluid
          >
          <v-row
            justify="center"
            align="center"
          >
            <v-col
              col="0"
							sm="8"
							md="10"
            >

            
              <v-dialog v-model="dialog" persistent max-width="600px">
                <template v-slot:activator="{ on, attrs }">
                  <v-btn
                    color="#1F2833"
                    dark
                    v-bind="attrs"
                    v-on="on"
                  > 
                  <v-icon
                    color="#66FCF1"
                  >
                    mdi-pencil-plus
                  </v-icon>
                    </v-btn>
                </template>
                   <v-alert v-if="message" :type="successful ? 'success' : 'error'">{{message}}</v-alert>
                   <form name="form" @submit.prevent="addActivity">
                    <v-card>
                      <v-card-title>
                        <span class="headline">New Schedule/Activity</span>
                      </v-card-title>
                      <v-card-text>
                        <v-container>
                          <v-row>
                            <v-col cols="12" sm="6" md="6">
                              <v-text-field
                                v-model="activity.eventActivity"
                                v-validate="'required|min:1|max:100'"
                                name ="eventActivity" 
                                label="Activity"
                              >
                              </v-text-field>
                            </v-col>
                            <v-col cols="12" sm="6" md="6">
                            <v-text-field
                                label="Details"
                                v-model="activity.details"
                                v-validate="'required|min:1|max:100'"
                                name="details"
                                hint="example of persistent helper text"
                                persistent-hint
                                required
                            >
                            </v-text-field>
                            </v-col>
                            <v-col cols="12" sm="6" md="4">
                              <v-text-field 
                                v-model="activity.setting"
                                v-validate="'required|min:1|max:100'"
                                name="setting"
                                label="Setting"
                                required
                              >
                              </v-text-field>
                            </v-col>
                            <v-col cols="12" sm="6" md="4">
                            <v-text-field
                              v-model="activity.dateTimeStart"
                              v-validate="'required'"
                              name="dateTimeStart" 
                              type="date"
                              label="Date Start" 
                              hint="example of helper text only on focus"
                            >
                            </v-text-field>
                            </v-col>
                            <v-col cols="12" sm="6" md="4">
                              <v-text-field
                                v-model="activity.dateTimeEnd"
                                v-validate="'required'"
                                label=  "Date End"
                                type="date"
                                hint="example of persistent helper text"
                                persistent-hint
                                required
                              >
                              </v-text-field>
                            </v-col>
                          </v-row>
                        </v-container>
                        <small>*indicates required field</small>
                      </v-card-text>
                      <v-card-actions>
                        <v-spacer></v-spacer>
                        <v-btn
                          
                          color="blue darken-1" 
                          text @click="dialog = false"
                        >
                          Close
                        </v-btn>
                        <v-btn
                          v-if ="this.update = false"
                          type="submit"
                          color="blue darken-1" 
                          text @click="dialog = false"
                        >
                          Save
                        </v-btn>
                        <v-btn
                          v-else type="button"
                          v-on:click="updateData()"
                          color="blue darken-1" 
                          text @click="dialog = false"
                        >
                          update
                        </v-btn>
                      </v-card-actions>
                    </v-card>
                  </form>
                </v-dialog>




              <v-card
                class="mx-auto"
              >
                <v-list two-line>
                  <v-list-item-group
                    v-model="selected"
                    multiple
                    active-class="pink--text"
                  >
                    <template v-for="(activity, index) in activities.slice().reverse()">
                      <v-list-item :key="'activity'+index">
                        <template v-slot:default="">
                          <v-list-item-action>
                            <v-card
                              class="main_card"
                              color="#385F73"
                              max-width="80"
                              max-height="80"
                              dark
                            >
                            <v-card-subtitle class="card_month">{{getMonthDate(activity.dateTimeStart)}}</v-card-subtitle>
                            <v-card-subtitle class="card_date">{{getDayDate(activity.dateTimeStart)}}</v-card-subtitle>
                            <v-card-actions>
                              <v-btn text>Listen Now</v-btn>
                             </v-card-actions>
                                </v-card>
                          </v-list-item-action>
                          <v-list-item-content>
                            <v-list-item-title v-text="activity.eventActivity"></v-list-item-title>
                            <v-list-item-subtitle class="text--primary" v-text="activity.details"></v-list-item-subtitle>
                            <v-list-item-subtitle v-text="activity.setting"></v-list-item-subtitle>
                          </v-list-item-content>
                          <v-list-item-action>
                           
                            <div class="text-center">
                              <v-btn class="mx-2" fab dark small 
                                color="primary"
                                v-on:click = "editAct(activity, activity.actId)"
                                
                                >
                                <v-icon dark>mdi-square-edit-outline</v-icon>
                              </v-btn>
                              <v-btn class="mx-2" fab dark small 
                                color="pink"
                                v-on:click = "deleteAct(activity.actId)"
                                >
                                <v-icon dark>mdi-delete-forever</v-icon>
                              </v-btn>
                            </div>
                          </v-list-item-action>
                        </template>
                      </v-list-item>
                      <v-divider
                        v-if="index + 1 < activities.length"
                        :key="index"
                      >
                      </v-divider>
                    </template>
                  </v-list-item-group>
                </v-list>
              </v-card>
            </v-col>
          </v-row>
        </v-container>
      </v-main>
    </v-app>  
  </div>
</template>

<script>
import moment from 'moment';
import Activity from '../models/activity'
import UserService from '../services/user-service';

export default {
  name: "Home",

     data: () => ({
      activity: new Activity("", "", "", "", ""),
      dialog: false,
      activities: '',
      selected: [2],
      submitted: false,
      successful: false,
      message : '',
    }),

  computed: {
    currentUser() {
      return this.$store.state.auth.user;
    }
  },
  mounted() {
     if (!this.currentUser) {
      this.$router.push('/login')
     }
      this.getAct();
  },

  methods: {
    moment: function () {
      return moment();
    },
  getAct(){
     UserService.getUserActivity(this.$store.state.auth.user.userId).then(
      response => {
        this.activities = response.data;
    },
      error => {
        this.content =
          (error.response && error.response.data) ||
          error.message ||
          error.toString();
        }
      );
  },
  
  getMonthDate: function (date) {
      return moment(date, 'YYYY-MM-DD').format('MMM');
  },

  getDayDate: function (date) {
      return moment(date, 'YYYY-MM-DD').format('DD');
  },

  addActivity() {
    this.message = "";
    this.submitted = true;
    this.$validator.validate().then((isValid) => {
      if (isValid) {
        UserService.addNewActivity(this.$store.state.auth.user.userId, this.activity).then(
          (data) => {
            this.getAct();
            this.message = data.message;
            this.successful = true;  
          },
          (error) => {
            this.successful = false;
            this.message = '';
            error.response.data.message
        });
      }
    });
  },
  deleteAct(activityId) {
    UserService.deleteActivity(activityId).then(
      (data) => {
        this.getAct();
        this.message = data.message;
        this.succesful = true;
      }).catch(
        (error) => {
          this.successful = false;
          this.message = '';
          error.response.data.message
        }
      )
    },
    editAct(activity) {
      this.activity.eventActivity = activity.eventActivity;
      this.activity.details = activity.details;
      this.activity.setting = activity.setting;
      this.activity.dateTimeStart = activity.dateTimeStart;
      this.activity.dateTimeEnd =activity.dateTimeEnd;
      this.isEdit = true;
      this.dialog = true;
    }
  }
}
</script>

<style scoped>
#container {
  margin: -20px auto;
}

.main_card {
  padding: 0;
}

.card_month {
  font-size: 1.0em;
  padding-bottom: 0;
  text-align: center;
}
.card_date {
  font-size: 0.8em;
  padding-top: 7px;
  text-align: center;
  
}

</style>