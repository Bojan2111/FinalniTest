import Navbar from "./components/UI/Navbar";
import Content from "./components/UI/Content";
import Footer from "./components/UI/Footer";
import "./App.css";

const App = () => {
  return (
    <div className="App">
      <header>
        <Navbar />
      </header>
      <Content />
      <Footer />
    </div>
  );
};

export default App;
