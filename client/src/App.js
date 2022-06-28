import React, { useEffect, useState } from 'react';

function App() {
  let [levels, setLevels] = useState([]);
  let [level, setLevel] = useState("");
  let [chatEvents, setChatEvents] = useState([]);

  const url = 'https://localhost:5001/api/chat-history';

  useEffect(() => {
    const response = fetch(`${url}/granularity`);

    response.then(res => res.json()).then(data => {
      setLevels(data);
    }).catch(err => {
      console.log(err);
    });
  }, []);

  const onSelectChange = async (e) => {
    setLevel(e.target.value);

    if (!e.target.value)
      return alert("Choose granularity");

    const response = await fetch(`${url}?granularity=${e.target.value}`);
    const events = await response.json();

    setChatEvents(events);
  }


  return (
    <div className='row'>
      <div className="landing">

        <div className="form-group">
          <label htmlFor="select1" className='heading-tertiary'>Select Granularity Level</label>
          <select value={level} onChange={onSelectChange} className="form-control">
            <option value="select">Select an Option</option>
            {levels.map(level => (
              <option key={level.id} value={level.id}>{level.name}</option>
            ))}
          </select>
        </div>

        {chatEvents && chatEvents.length > 0 &&
          chatEvents.map((event, index) => (
            <div className="landing__body" key={index}>
              <div className='landing__time'>
                <h2 className='landing__time--item'>{event.time}:</h2>
              </div>
              <div className='landing__events'>
                {event.events.map((item, index) => (
                  <h2 className='landing__events--item' key={index}>
                    {item}
                  </h2>
                ))}
              </div>
            </div>
          ))}
      </div>
    </div>
  );
}

export default App;
