import { useEffect, useState } from "react"
import "./App.css"
import axios from "axios";
import { Header, List } from "semantic-ui-react";

function App() {
  const [activities, setActivities] = useState([]);

  // axios.get 을 사용하여 ActivitiesController.cs 에서 설정해놓은 GET 요청을 가져오고, 그 값을 useState 의 setActivities 를 통해 activities 에 저장함
  useEffect(() => {
    axios.get("http://localhost:5000/api/activities")
      .then(response => {
        console.log(response);
        setActivities(response.data)
      })
  }, [])

  return (
    // 이렇게 tsx 혹은 js 파일 안에서 return 값 안에 있는 HTML 형식을 가진 코드들을 JSX 형식이라고 부름.
    // return 내에서는 오로지 하나의 요소만 존재할 수 있음. <h1> 과 <div> 가 따로 존재하면 오류.
    <div>
      <Header as="h2" icon="users" content="Reactivities" />

      <List>
        {/* useEffect 에서 저장해놓은 값들을 웹페이지에 display */}
        {activities.map((activity: any) => (
          <List.Item key={activity.id}>
            {activity.title}
          </List.Item>
        ))}
      </List>
    </div>
  )
}

export default App