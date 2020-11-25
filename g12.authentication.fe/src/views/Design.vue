<template>
  <div class="m-5">
    <h2>Header</h2>
    <h1>This is H1</h1>
    <h2>This is H2</h2>
    <h3>This is H3</h3>
    <h4>This is H4</h4>
    <h5>This is H5</h5>
    <h6>This is H6</h6>
    <p>This is P</p>
    <h2>Button</h2>
    <div>
      <button type="button" class="btn btn-primary mr-2">Primary</button>
      <button type="button" class="btn btn-secondary mr-2">Secondary</button>
      <button type="button" class="btn btn-success mr-2">Success</button>
      <button type="button" class="btn btn-danger mr-2">Danger</button>
      <button type="button" class="btn btn-warning mr-2">Warning</button>
      <button type="button" class="btn btn-info mr-2">Info</button>
      <button type="button" class="btn btn-light mr-2">Light</button>
      <button type="button" class="btn btn-dark mr-2">Dark</button>
      <button type="button" class="btn btn-link">Link</button>
    </div>
    <h2>Form</h2>
    <div class="mt-3">
      <form>
        <div class="form-group">
          <input type="text" class="form-control" />
        </div>
      </form>
    </div>
    <h2>Alert</h2>
    <div class="mt-3">
      <div class="alert alert-primary" role="alert">
        <strong>primary</strong>
      </div>
      <div class="alert alert-success" role="alert">
        <strong>success</strong>
      </div>
      <div class="alert alert-warning" role="alert">
        <strong>warning</strong>
      </div>
      <div class="alert alert-danger" role="alert">
        <strong>danger</strong>
      </div>
    </div>
    <h2>Modal</h2>
    <button class="btn btn-primary" @click="showModal = true">Open Modal</button>
    <modal v-if="showModal" @close="showModal = false">
      <!--
      you can use custom content here to overwrite
      default content
      -->
      <h3 slot="header">custom header</h3>
    </modal>
    <h2>Badge</h2>
    <div class="mt-3">
      <span class="badge badge-primary">primary</span>
      <span class="badge badge-secondary">secondary</span>
      <span class="badge badge-success">success</span>
      <span class="badge badge-warning">warning</span>
      <span class="badge badge-danger">danger</span>
    </div>
    <h2>Toastr</h2>
    <div class="mt-3">
      <button class="btn btn-primary" @click="showToastr">Toastr</button>
    </div>
    <h2>CheckBox And Radio</h2>
    <div>
      <div class="nvk__radio">
        <input type="radio" checked id="red" />
        <label for="red">Radio</label>
      </div>

      <div class="nvk__radio">
        <input type="checkbox" checked id="red2" />
        <label for="red2">Checkbox</label>
      </div>
    </div>
    <h2>Date Picker</h2>
    <date-picker v-model="exampleTime"></date-picker>
    <h2>Paganization</h2>
    <nav aria-label="Page navigation example">
      <ul class="pagination">
        <li class="page-item">
          <a class="page-link" href="#">Previous</a>
        </li>
        <li class="page-item">
          <a class="page-link" href="#">1</a>
        </li>
        <li class="page-item active">
          <a class="page-link" href="#">2</a>
        </li>
        <li class="page-item">
          <a class="page-link" href="#">3</a>
        </li>
        <li class="page-item">
          <a class="page-link" href="#">Next</a>
        </li>
      </ul>
    </nav>
    <h2>Text editer</h2>
    <div class="p-3 border">
      <editor-menu-bar :editor="editor" v-slot="{ commands, isActive }">
        <button :class="{ 'is-active': isActive.bold() }" @click="commands.bold">Bold</button>
      </editor-menu-bar>
      <editor-content :editor="editor" />
    </div>
    <h2>Muliti select</h2>
    <div>
      <multiselect v-model="mulitiSelect" :options="options"></multiselect>
    </div>
    <h2>Image</h2>
    <img src="http://localhost:62940/Files/TP101TQ6B-US6E0KJC8-d644b60a2cdb-512.jpg" alt="">
  </div>
</template>

<script>
import Modal from "@/components/Modal";
import Toastr from "@/plugins/toastr";
import DatePicker from "vue2-datepicker";
import "@/styles/common/datepicker.scss";
import "vue2-datepicker/locale/vi";
import { Editor, EditorContent, EditorMenuBar } from "tiptap";
import {
  Blockquote,
  CodeBlock,
  HardBreak,
  Heading,
  OrderedList,
  BulletList,
  ListItem,
  TodoItem,
  TodoList,
  Bold,
  Code,
  Italic,
  Link,
  Strike,
  Underline,
  History,
} from "tiptap-extensions";
import Multiselect from "@/components/common/mulitiSelect";
export default {
  name: "Design",
  data() {
    return {
      showModal: false,
      exampleTime: null,
      hello: "",
      editor: null,
      mulitiSelect: null,
      options: ["list", "of", "options"],
    };
  },
  methods: {
    showToastr() {
      Toastr.s("Hello Toastr");
    },
  },
  components: {
    modal: Modal,
    DatePicker,
    EditorContent,
    EditorMenuBar,
    Multiselect,
  },
  mounted() {
    this.editor = new Editor({
      extensions: [
        new Blockquote(),
        new CodeBlock(),
        new HardBreak(),
        new Heading({ levels: [1, 2, 3] }),
        new BulletList(),
        new OrderedList(),
        new ListItem(),
        new TodoItem(),
        new TodoList(),
        new Bold(),
        new Code(),
        new Italic(),
        new Link(),
        new Strike(),
        new Underline(),
        new History(),
      ],
      content: "<p>This is just a boring paragraph</p>",
    });
  },
};
</script>

<style scoped>
</style>